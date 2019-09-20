using AutoMapper;
using Core.Modules;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Infrastructure;
using Infrastructure.Interface;
using Infrastructure.Security;
using Infrastructure.Utility;
using MiniBar.Common.Workitems.Main;
using MiniBar.EntityViewModels.Global;
using MiniBar.Modules;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Infrastructure.Framework.PrismApplication
    {
        ShellWindow ShellWindow;
        ShellWindowViewModel ShellViewModel;

        protected override Window CreateShell()
        {
            ShellWindow = Container.Resolve<ShellWindow>();
            ShellViewModel = (ShellWindowViewModel)ShellWindow.DataContext;
            ((IContainerRegistry)Container).RegisterInstance<IShell>(ShellViewModel);
            return ShellWindow;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            MainWindow?.Hide();
            CommandManager.RegisterCommand(MiniBar.Common.Constants.CommandNames.Exit, Application.Current.MainWindow.Close);
            Container.Resolve<IContextService>().LaunchWorkItem<MainWorkitem>();
        }

        protected override void ConfigureMappings(IMapperConfigurationExpression cfg)
        {
            base.ConfigureMappings(cfg);
            cfg.AddMaps(typeof(ImageViewModel).Assembly);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            var moduleManager = Container.Resolve<IModuleManager>();
            moduleManager.LoadModuleCompleted += (o, e) =>
            {
                try
                {
                    DXSplashScreen.SetState(String.Format("Loading {0}", e.ModuleInfo.ModuleName));
                }
                catch
                {

                }
            };
            moduleCatalog
                .AddModule<SecurityModule>()
                .AddModule<CommonModule>()
                .AddModule<ConfigurationModule>()
                .AddModule<ProductsModule>();
                //.AddModule<AnalyticsModule>();

        }

        protected override void OnStartup(StartupEventArgs eventArgs)
        {

            DXSplashScreen.Show<SplashScreen>();
            base.OnStartup(eventArgs);

            Current.Dispatcher.Invoke(() =>
            {
                DXSplashScreen.Close();
                if (MainWindow != null)
                {
                    MainWindow.Show();
                    UIHelper.TryFocusWindow(MainWindow, true);
                }
            });

            CultureInfo culture = (CultureInfo)CultureInfo.GetCultureInfo("en").Clone();
            culture.NumberFormat.CurrencySymbol = "֏";
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

        }
    }
}
