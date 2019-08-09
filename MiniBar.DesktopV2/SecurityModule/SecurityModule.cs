using DevExpress.Mvvm;
using Infrastructure;
using Infrastructure.Utility;
using Prism.Ioc;
using Security;
using Security.Constants;
using Security.Internal.Controllers;
using Security.Internal.Services;
using Security.Workitems.Login;
using Security.Workitems.SoftwareUpdate;
using System;
using System.Threading.Tasks;

namespace Core.Modules
{
    public class SecurityModule : Module
    {
        public override string Name => "Security";
        
        private IContainerProvider Container { get; set; }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            Container = containerProvider;
            CommandManager.RegisterCommand(CommandNames.Login, new AsyncCommand(OpenLoginWorkitem));
            CommandManager.RegisterCommand(CommandNames.Logout, new AsyncCommand(Logout));
            CommandManager.RegisterCommand(CommandNames.CheckSoftwareUpdate, new AsyncCommand(OpenUpdateWorkitem));
        }

        private async Task OpenUpdateWorkitem()
        {
            await CurrentContextService.LaunchModalWorkItem<SoftwareUpdateWorkitem>();
        }

        private async Task OpenLoginWorkitem()
        {
            await CurrentContextService.LaunchModalWorkItem<LoginWorkitem>();
        }

        private async Task Logout()
        {
            AuthenticationController controller = Container.Resolve<AuthenticationController>();
            try
            {
                await controller.Logout();
            }
            catch(Exception ex)
            {
                ApiHelper.HandleApiException(ex);
            }
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<AuthenticationController>();
        }

    }
}
