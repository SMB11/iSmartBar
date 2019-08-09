using DevExpress.Mvvm;
using DevExpress.Spreadsheet;
using Infrastructure.Framework;
using Infrastructure.Utility;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MiniBar.Common.Workitems.ImportExcel.Views
{
    public class ImportViewModel : BaseViewModel
    {
        public ImportExcelOptions ImportExcelOptions { get; set; }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private AsyncCommand saveTemplateCommand;
        public AsyncCommand SaveTemplateCommand =>
            saveTemplateCommand ?? (saveTemplateCommand = new AsyncCommand(ExecuteSaveTemplateCommand, CanExecuteSaveTemplateCommand));


        async Task ExecuteSaveTemplateCommand()
        {

            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = true;
            if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IsTemplateLoading = true;
                string filename =(ImportExcelOptions.TemplateName ?? "importTemplate") + ".xlsx";

                string filePath = FileHelper.GetAvailableFilename(Path.Combine(folderDialog.SelectedPath, filename));

                await Task.Run(() =>
                {

                    Stream stream = ImportExcelOptions.GetTemplateStreamFunc(DocumentFormat.Xlsx);
                    try
                    {
                        if (FileHelper.TrySaveStreamToFile(stream, filePath, FileMode.CreateNew) == false)
                            return;

                    }
                    catch (IOException)
                    {
                        UIManager.Error("Unauthorized to access the specified folder");
                        return;
                    }
                    if (UIManager.ShowMessageBox("Open the saved file?", "Open Template", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                        OpenExcelFile(filePath);
                });
                IsTemplateLoading = false;
            }

        }


        private bool CanExecuteSaveTemplateCommand()
        {
            return !IsTemplateLoading;
        }

        void OpenExcelFile(string filename)
        {
            try
            {

                Process excel = new Process();
                excel.StartInfo.FileName = filename;
                excel.Start();

                // Need to wait for excel to start
                excel.WaitForInputIdle();

                IntPtr p = excel.MainWindowHandle;
                ShowWindow(p, 1);
            }
            catch
            {

                UIManager.Error("Failed to open file.");
            }
        }

        public ICommand<string> ImportFromFileCommand { get; set; }

        private DelegateCommand openFileCommand;
        public DelegateCommand OpenFileCommand =>
            openFileCommand ?? (openFileCommand = new DelegateCommand(ExecuteOpenFileCommand, CanExecuteOpenFileCommand));

        void ExecuteOpenFileCommand()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.CheckFileExists = true;
            fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            if (fileDialog.ShowDialog() == true)
            {
                FilePath = fileDialog.FileName;
            }
        }

        private bool CanExecuteOpenFileCommand()
        {
            return !IsTemplateLoading;
        }

        private bool CanExecuteFinishImportCommand()
        {
            return !IsTemplateLoading;
        }

        private string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value, nameof(FilePath)); }
        }


        private bool isTemplateLoading;

        public bool IsTemplateLoading
        {
            get { return isTemplateLoading; }
            set {
                SetProperty(ref isTemplateLoading, value, nameof(IsTemplateLoading));
                SaveTemplateCommand.RaiseCanExecuteChanged();
                OpenFileCommand.RaiseCanExecuteChanged();
            }
        }

    }
}
