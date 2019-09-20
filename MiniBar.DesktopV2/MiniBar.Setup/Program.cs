using Microsoft.Deployment.WindowsInstaller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WixSharp;

namespace MiniBar.Setup
{
    class Program
    {
        static List<string> modules = new List<string>();//{ "MiniBar.ProductsModule.dll", "MiniBar.ProductsModule.dll.config", "MiniBar.ConfigurationModule.dll", "MiniBar.ConfigurationModule.dll.config", "MiniBar.AnalyticsModule.dll", "MiniBar.AnalyticsModule.dll.config" };

        static public void Main(string[] args)
        {
            var desktopShortcut = new ExeFileShortcut("Minibar Admin", "[INSTALLDIR]MiniBarManager.exe", "");
            var project = new Project("MiniBar Admin",
                new Dir(@"%ProgramFiles%/MyBar/Admin",
                    new DirFiles(@"D:\iSmartBar\MiniBar.DesktopV2\Shell\bin\Release\*", MainFolderFileFilter),
                    new Dir(@"bin",
                            new DirFiles(@"D:\iSmartBar\MiniBar.DesktopV2\Shell\bin\Release\*.dll", BinFolderDLLFilter)),

                    new Dir(@"%Desktop%", desktopShortcut),
                    new Dir(@"Modules",
                            new DirFiles(@"D:\iSmartBar\MiniBar.DesktopV2\Shell\bin\Release\*", ModulesFilter))
                ));
            project.LicenceFile = @"D:\iSmartBar\MiniBar.DesktopV2\MiniBar.Setup\gpl-3.0.rtf";
            project.UI = WUI.WixUI_InstallDir;

            project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");

            project.Actions = new WixSharp.Action[] {
                 new ElevatedManagedAction(CustomActions.StartService, Return.asyncWait, When.After, Step.MoveFiles, WixSharp.Condition.NOT_Installed)
            };
            Compiler.BuildMsi(project);
        }

        private static bool ModulesFilter(string file)
        {
            if (modules.Contains(Path.GetFileName(file)))
                return true;
            return false;
        }

        private static bool BinFolderDLLFilter(string file)
        {
            if(file.EndsWith(".pdb") || file.EndsWith(".xml") || file.EndsWith(".pssym"))
                return false;
            if (Path.GetFileName(file).StartsWith("MiniBar"))
                return false;
            if (modules.Contains(Path.GetFileName(file)))
                return false;
            return true;
        }

        private static bool MainFolderFileFilter(string file)
        {
            if (file.EndsWith(".pdb") || file.EndsWith(".xml") || file.EndsWith(".pssym"))
                return false;

            if (modules.Contains(Path.GetFileName(file)))
                return false;

            if (Path.GetFileName(file).StartsWith("MiniBar"))
                return true;

            return false;
        }
    }

    public class CustomActions
    {

        [CustomAction]
        public static ActionResult StartService(Session session)
        {

            string installDir = session.Property("INSTALLDIR");

            //MessageBox.Show(installDir, "Embedded Managed CA");

            return ActionResult.Success;
        }
    }
}
