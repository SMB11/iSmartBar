using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DXInfrastructure;
using Infrastructure.Constants;

namespace Shell
{
    class ShellWindowViewModel : BindableBase
    {

        public ShellWindowViewModel()
        {

            BarCommandManager.RegisterCommand(CommandNames.SetTouchTheme, new DelegateCommand(() => ChangeTheme(Theme.Office2016WhiteTouchName)));
            BarCommandManager.RegisterCommand(CommandNames.SetDarkTheme, new DelegateCommand(() => ChangeTheme(Theme.VS2017DarkName)));
            BarCommandManager.RegisterCommand(CommandNames.SetLightTheme, new DelegateCommand(() => ChangeTheme(Theme.VS2017LightName)));
        }

        private void ChangeTheme(string theme)
        {
            ApplicationThemeHelper.ApplicationThemeName = theme;
        }
    }
}
