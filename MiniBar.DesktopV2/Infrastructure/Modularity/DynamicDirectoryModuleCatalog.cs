﻿using CommonServiceLocator;
using Infrastructure.Logging;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Threading;
using System.Windows;

namespace Infrastructure.Modularity
{
    public class DynamicDirectoryModuleCatalog : ModuleCatalog
    {

        SynchronizationContext _context;
        /// <summary>
        /// Directory containing modules to search for.
        /// </summary>
        public string ModulePath { get; set; }

        /// <summary>
        /// Path to Bin folder for resolving dependant assemblies
        /// </summary>
        public string BinPath { get; set; }

        public DynamicDirectoryModuleCatalog(string modulePath, string binDirectory)
        {
            _context = SynchronizationContext.Current;
            ModulePath = modulePath;
            BinPath = binDirectory;
            CreateFileWatcher();
        }

        void CreateFileWatcher()
        {
            if (Directory.Exists(ModulePath))
                DoCreateFileWatcher();

        }

        void DoCreateFileWatcher()
        {
            FileSystemWatcher fileWatcher = null;
            // we need to watch our folder for newly added modules
            fileWatcher = new FileSystemWatcher(ModulePath, "*.dll");
            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Rasied when a new file is added to the ModulePath directory
        /// </summary>
        void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                LoadModuleCatalog(e.FullPath, true);
            }
        }

        /// <summary>
        /// Drives the main logic of building the child domain and searching for the assemblies.
        /// </summary>
        protected override void InnerLoad()
        {
            try
            {
                LoadModuleCatalog(ModulePath);
            }
            catch
            {
            }
        }

        void LoadModuleCatalog(string path, bool isFile = false)
        {
            if (string.IsNullOrEmpty(path))
                throw new InvalidOperationException("Path cannot be null.");

            if (isFile)
            {
                if (!File.Exists(path))
                    throw new InvalidOperationException(string.Format("File {0} could not be found.", path));
            }
            else
            {
                if (!Directory.Exists(path))
                    throw new InvalidOperationException(string.Format("Directory {0} could not be found.", path));
            }

            AppDomain childDomain = this.BuildChildDomain(AppDomain.CurrentDomain);

            try
            {
                List<string> loadedAssemblies = new List<string>();

                var assemblies = (
                                     from Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()
                                     where !(assembly is System.Reflection.Emit.AssemblyBuilder)
                                        && assembly.GetType().FullName != "System.Reflection.Emit.InternalAssemblyBuilder"
                                        && !String.IsNullOrEmpty(assembly.Location)
                                     select assembly.Location
                                 );

                loadedAssemblies.AddRange(assemblies);

                Type loaderType = typeof(InnerModuleInfoLoader);
                if (loaderType.Assembly != null)
                {
                    var loader = (InnerModuleInfoLoader)childDomain.CreateInstanceFrom(loaderType.Assembly.Location, loaderType.FullName).Unwrap();
                    foreach (var assembly in loadedAssemblies)
                    {
                        try
                        {
                            loader.LoadAssemblies(new string[] { assembly });
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    //get all the ModuleInfos
                    ModuleInfo[] modules = loader.GetModuleInfos(path, isFile);

                    //add modules to catalog
                    this.Items.AddRange(modules);

                    //we are dealing with a file from our file watcher, so let's notify that it needs to be loaded
                    if (isFile)
                    {
                        LoadModules(modules);
                    }
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                AppDomain.Unload(childDomain);
            }
        }

        /// <summary>
        /// Uses the IModuleManager to load the modules into memory
        /// </summary>
        /// <param name="modules"></param>
        private void LoadModules(ModuleInfo[] modules)
        {
            if (_context == null)
                return;

            IModuleManager manager = ServiceLocator.Current.GetInstance<IModuleManager>();

            _context.Send(new SendOrPostCallback(delegate (object state)
            {
                foreach (var module in modules)
                {
                    manager.LoadModule(module.ModuleName);
                }
            }), null);
        }

        /// <summary>
        /// Creates a new child domain and copies the evidence from a parent domain.
        /// </summary>
        /// <param name="parentDomain">The parent domain.</param>
        /// <returns>The new child domain.</returns>
        /// <remarks>
        /// Grabs the <paramref name="parentDomain"/> evidence and uses it to construct the new
        /// <see cref="AppDomain"/> because in a ClickOnce execution environment, creating an
        /// <see cref="AppDomain"/> will by default pick up the partial trust environment of 
        /// the AppLaunch.exe, which was the root executable. The AppLaunch.exe does a 
        /// create domain and applies the evidence from the ClickOnce manifests to 
        /// create the domain that the application is actually executing in. This will 
        /// need to be Full Trust for Composite Application Library applications.
        /// </remarks>
        /// <exception cref="ArgumentNullException">An <see cref="ArgumentNullException"/> is thrown if <paramref name="parentDomain"/> is null.</exception>
        protected virtual AppDomain BuildChildDomain(AppDomain parentDomain)
        {
            if (parentDomain == null) throw new System.ArgumentNullException("parentDomain");

            Evidence evidence = new Evidence(parentDomain.Evidence);
            AppDomainSetup setup = parentDomain.SetupInformation;
            return AppDomain.CreateDomain("DiscoveryRegion", evidence, setup);
        }

        private class InnerModuleInfoLoader : MarshalByRefObject
        {
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
            internal ModuleInfo[] GetModuleInfos(string path, bool isFile = false)
            {
                Assembly moduleReflectionOnlyAssembly =
                    AppDomain.CurrentDomain.GetAssemblies().First(
                        asm => asm.FullName == typeof(IModule).Assembly.FullName);

                Type IModuleType = moduleReflectionOnlyAssembly.GetType(typeof(IModule).FullName);

                FileSystemInfo info = null;
                if (isFile)
                    info = new FileInfo(path);
                else
                    info = new DirectoryInfo(path);

                IEnumerable<ModuleInfo> modules = GetNotAllreadyLoadedModuleInfos(info, IModuleType);

                return modules.ToArray();
            }

            private static IEnumerable<ModuleInfo> GetNotAllreadyLoadedModuleInfos(FileSystemInfo info, Type IModuleType)
            {
                List<FileInfo> validAssemblies = new List<FileInfo>();
                Assembly[] alreadyLoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

                FileInfo fileInfo = info as FileInfo;
                if (fileInfo != null)
                {
                    if (alreadyLoadedAssemblies.FirstOrDefault(assembly => String.Compare(Path.GetFileName(assembly.Location), fileInfo.Name, StringComparison.OrdinalIgnoreCase) == 0) == null)
                    {
                        var moduleInfos = Assembly.LoadFile(fileInfo.FullName).GetExportedTypes()
                        .Where(IModuleType.IsAssignableFrom)
                        .Where(t => t != IModuleType)
                        .Where(t => !t.IsAbstract).Select(t => CreateModuleInfo(t));

                        return moduleInfos;
                    }
                }

                DirectoryInfo directory = info as DirectoryInfo;

                var files = directory.GetFiles("*.dll").Where(file => alreadyLoadedAssemblies.
                    FirstOrDefault(assembly => String.Compare(Path.GetFileName(assembly.Location), file.Name, StringComparison.OrdinalIgnoreCase) == 0) == null);

                foreach (FileInfo file in files)
                {
                    try
                    {
                        Assembly.LoadFile(file.FullName);
                        validAssemblies.Add(file);
                    }
                    catch (BadImageFormatException)
                    {
                        // skip non-.NET Dlls
                    }
                }

                return validAssemblies.SelectMany(file => Assembly.LoadFile(file.FullName)
                                            .GetExportedTypes()
                                            .Where(IModuleType.IsAssignableFrom)
                                            .Where(t => t != IModuleType)
                                            .Where(t => !t.IsAbstract)
                                            .Select(type => CreateModuleInfo(type)));
            }


            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
            internal void LoadAssemblies(IEnumerable<string> assemblies)
            {
                foreach (string assemblyPath in assemblies)
                {
                    try
                    {
                        Assembly.LoadFile(assemblyPath);
                    }
                    catch (FileNotFoundException)
                    {
                        // Continue loading assemblies even if an assembly can not be loaded in the new AppDomain
                    }
                }
            }

            private static ModuleInfo CreateModuleInfo(Type type)
            {
                string moduleName = type.Name;
                List<string> dependsOn = new List<string>();
                bool onDemand = false;
                var moduleAttribute = CustomAttributeData.GetCustomAttributes(type).FirstOrDefault(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleAttribute).FullName);

                if (moduleAttribute != null)
                {
                    foreach (CustomAttributeNamedArgument argument in moduleAttribute.NamedArguments)
                    {
                        string argumentName = argument.MemberInfo.Name;
                        switch (argumentName)
                        {
                            case "ModuleName":
                                moduleName = (string)argument.TypedValue.Value;
                                break;

                            case "OnDemand":
                                onDemand = (bool)argument.TypedValue.Value;
                                break;

                            case "StartupLoaded":
                                onDemand = !((bool)argument.TypedValue.Value);
                                break;
                        }
                    }
                }

                var moduleDependencyAttributes = CustomAttributeData.GetCustomAttributes(type).Where(cad => cad.Constructor.DeclaringType.FullName == typeof(ModuleDependencyAttribute).FullName);
                foreach (CustomAttributeData cad in moduleDependencyAttributes)
                {
                    dependsOn.Add((string)cad.ConstructorArguments[0].Value);
                }

                ModuleInfo moduleInfo = new ModuleInfo(moduleName, type.AssemblyQualifiedName)
                {
                    InitializationMode =
                        onDemand
                            ? InitializationMode.OnDemand
                            : InitializationMode.WhenAvailable,
                    Ref = type.Assembly.CodeBase,
                };
                moduleInfo.DependsOn.AddRange(dependsOn);
                return moduleInfo;
            }
        }
    }

    /// <summary>
    /// Class that provides extension methods to Collection
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Add a range of items to a collection.
        /// </summary>
        /// <typeparam name="T">Type of objects within the collection.</typeparam>
        /// <param name="collection">The collection to add items to.</param>
        /// <param name="items">The items to add to the collection.</param>
        /// <returns>The collection.</returns>
        /// <exception cref="System.ArgumentNullException">An <see cref="System.ArgumentNullException"/> is thrown if <paramref name="collection"/> or <paramref name="items"/> is <see langword="null"/>.</exception>
        public static Collection<T> AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
        {
            if (collection == null) throw new System.ArgumentNullException("collection");
            if (items == null) throw new System.ArgumentNullException("items");

            foreach (var each in items)
            {
                collection.Add(each);
            }

            return collection;
        }
    }
}
