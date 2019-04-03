using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core;
using DXInfrastructure;
using Infrastructure.Constants;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell
{
    class ShellWindowViewModel : BindableBase
    {
        public bool IsLightSkin
        {
            get
            {
                return ApplicationThemeHelper.ApplicationThemeName == Theme.VS2017LightName;
            }
        }

        public bool IsDarkSkin
        {
            get
            {
                return ApplicationThemeHelper.ApplicationThemeName == Theme.VS2017DarkName;
            }
        }

        public ShellWindowViewModel()
        {
            ThemeManager.ApplicationThemeChanged += ThemeManager_ApplicationThemeChanged;

            BarCommandManager.RegisterCommand(CommandNames.SetDarkTheme, new DelegateCommand(() => ChangeTheme(Theme.VS2017DarkName)));
            BarCommandManager.RegisterCommand(CommandNames.SetLightTheme, new DelegateCommand(() => ChangeTheme(Theme.VS2017LightName)));
        }

        private void ChangeTheme(string theme)
        {
            ApplicationThemeHelper.ApplicationThemeName = theme;
        }


        private void ThemeManager_ApplicationThemeChanged(System.Windows.DependencyObject sender, ThemeChangedRoutedEventArgs e)
        {
            this.RaisePropertiesChanged(nameof(IsLightSkin));
            this.RaisePropertiesChanged(nameof(IsDarkSkin));
        }
    }
}
