using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DXInfrastructure;
using Infrastructure.Helpers;
using Prism.Ioc;
using Security;
using Security.Api;
using Security.Constants;
using Security.Internal.Controllers;
using Security.Internal.Services;
using Security.Internal.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace MiniBar.Modules
{
    public class SecurityModule : Module
    {
        public override string Name => "Security";
        
        private IContainerProvider Container { get; set; }

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            Container = containerProvider;
            BarCommandManager.RegisterCommand(CommandNames.Login, Login);
            BarCommandManager.RegisterCommand(CommandNames.Logout, new AsyncCommand(Logout));
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

        private void Login()
        {
            AuthenticationController controller = Container.Resolve<AuthenticationController>();
            LoginView loginView = new LoginView();
            UICommand loginCommand = new UICommand
            {
                Caption = "Login",
                IsCancel = false,
                IsDefault = true,
                Command = new AsyncCommand<CancelEventArgs>(async (c) => {
                    try
                    {
                        c.Cancel = true;
                        await controller.AuthenticateAsync(loginView.Username, loginView.Password);
                        UIHelper.CloseCurrentDialog();
                    }
                    catch (ApiException apiEx)
                    {
                        loginView.ErrorText = apiEx.Message;
                    }

                    catch (ApiConnectionException)
                    {
                        loginView.ErrorText = "Couldn't connect to server";
                    }
                    catch
                    {
                        loginView.ErrorText = "An unkown error occured, please contact your administrator.";
                    }
                })
            };
            UIHelper.ShowModal(loginView, "Login", ResizeMode.NoResize, new Size(300,300), new List<UICommand> { loginCommand });
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<AuthenticationController>();
        }

    }
}
