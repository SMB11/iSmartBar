using DevExpress.Xpf.Core;
using MiniBar.Modules;
using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Windows;
using AutoMapper;
using System.Globalization;
using Infrastructure.Interface;
using Core.Modules;
using Infrastructure.Utility;
using MiniBar.EntityViewModels.Global;
using MiniBar.Common.Workitems.Main;
using Infrastructure.Security;
using System.Threading.Tasks;
using System.Threading;

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

            Container.Resolve<IContextService>().LaunchWorkItem<MainWorkitem>();
            AppSecurityContext.AppPrincipalChanged += AppSecurityContext_AppPrincipalChanged;
        }

        private void AppSecurityContext_AppPrincipalChanged(object sender, EventArgs e)
        {
            IContextService currentContextService = Container.Resolve<IContextService>();
            Task.Factory.StartNew(async () =>
            {
                Thread.Sleep(1000);
                for (int i = 0; i < 0; i++)
                {
                    //await currentContextService.LaunchModalWorkItem<CountryManagerWorkitem>();
                    //await currentContextService.LaunchModalWorkItem<CityManagerWorkitem>();
                    //await currentContextService.LaunchModalWorkItem<HotelManagerWorkitem>();
                    //await currentContextService.LaunchModalWorkItem<ProductManagerWorkitem>();
                    //await currentContextService.LaunchModalWorkItem<CategoryManagerWorkitem>();
                    //await currentContextService.LaunchModalWorkItem<BrandManagerWorkitem>();
                }
            });
        }
    }
}
