using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Constants
{
    public class CommandNames
    {
        public const string SetLightTheme = "SetLightThemeCommand";
        public const string SetDarkTheme = "SetDarkThemeCommand";
        public const string SetTouchTheme = "SetTouchThemeCommand";
        public const string OpenFile = "OpenFileCommand";
        public const string CloseWorkItem = "CloseWorkItemCommand";
        public const string FocusWorkitem = "FocusWorkitemCommand";
        public const string NewTab = "App.NewTabCommand";
        public const string Reconnect = "App.Reconect";
        public const string CloseAllTabs = "App.CloseAllTabs";
        
        public const string AddObject = "AddObject";
        public const string AddObjectCopy = "AddObjectCopy";
        public const string EditObject = "EditObject";
        public const string SaveObject = "SaveObject";
        public const string RemoveObject = "RemoveObject";
        public const string CancelEditingObject = "CancelEditingObject";
        
        public const string ImportFromExcel = "ImportFromExcel";
        public const string Accept = "Accept";

        public const string RefreshList = "RefreshList";
        public const string Search = "Search";
        public const string ExpandAll = "ExpandAll";
        public const string CollapseAll = "CollapseAll";

    }
}
