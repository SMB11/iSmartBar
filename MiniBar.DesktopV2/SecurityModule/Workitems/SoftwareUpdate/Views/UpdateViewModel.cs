using DevExpress.Mvvm;
using Infrastructure.Framework;
using Security.Services;
using SharedEntities.DTO.Updates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Security.Workitems.SoftwareUpdate.Views
{
    public class UpdateViewModel : WorkitemViewModel
    {
        UpdateService UpdateService;

        public UpdateViewModel()
        {
            UpdateService = new UpdateService();
            SearchForUpdates();
        }


        private ObservableCollection<AssemblyDTO> newModules;
        public ObservableCollection<AssemblyDTO> NewModules
        {
            get { return newModules; }
            set { SetProperty(ref newModules, value, nameof(NewModules)); }
        }

        private bool isUpdateAvailable;
        public bool IsUpdateAvailable
        {
            get { return isUpdateAvailable; }
            set { SetProperty(ref isUpdateAvailable, value, nameof(IsUpdateAvailable)); }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value, nameof(IsLoading)); }
        }

        public async void SearchForUpdates()
        {
            IsLoading = true;

            Assembly shellAssembly = typeof(Infrastructure.Workitems.Workitem).Assembly;
            int major = Assembly.GetEntryAssembly().GetName().Version.Major;
            List<AssemblyDTO> list = new List<AssemblyDTO>();
            try
            {
                list = await UpdateService.GetLatestFileInfo(1);
            }
            catch
            {
                list = new List<AssemblyDTO>();
            }
            finally
            {
                CalculateUpdates(list);
                IsLoading = false;
            }
        }

        private void CalculateUpdates(List<AssemblyDTO> list)
        {
            DirectoryInfo assembliesDir = new DirectoryInfo(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Modules"));
            if (assembliesDir != null && assembliesDir.Exists)
            {
                List<AssemblyDTO> localList = assembliesDir.GetFiles().Select(f =>
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = Assembly.LoadFrom(f.FullName);
                    }
                    catch { return null; }
                    Version version = assembly.GetName().Version;
                    return new AssemblyDTO
                    {
                        Name = f.Name,
                        Build = version.Build,
                        Major = version.Major,
                        Minor = version.Minor,
                        Revision = version.Revision,
                        FullName = assembly.FullName
                    };
                }).ToList();

                List<AssemblyDTO> needUpdate = new List<AssemblyDTO>();
                List<AssemblyDTO> newModules = new List<AssemblyDTO>();
                foreach (var item in list)
                {
                    AssemblyDTO assemblyDTO = localList.Find(d => d?.Name == item?.Name);
                    if (assemblyDTO != null)
                    {
                        if (assemblyDTO.GetVersion() != item.GetVersion())
                            needUpdate.Add(item);
                    }
                    else
                        newModules.Add(item);
                }
                SetUpdateData(newModules, needUpdate);
            }
        }

        private void SetUpdateData(List<AssemblyDTO> newModules, List<AssemblyDTO> needUpdate)
        {
            IsUpdateAvailable = false;
            //if (newModules.Count == 0)
            //{
            //    IsUpdateAvailable = false;
            //}
            //else
            //{
            //    IsUpdateAvailable = true;
            //    NewModules = new ObservableCollection<AssemblyDTO>(newModules);
            //}
        }

        private DelegateCommand downloadCommand;
        public DelegateCommand DownloadCommand =>
            downloadCommand ?? (downloadCommand = new DelegateCommand(ExecuteDownloadCommand, CanExecuteDownloadCommand));

        void ExecuteDownloadCommand()
        {

        }

        bool CanExecuteDownloadCommand()
        {
            return true;
        }
    }
}