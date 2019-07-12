using DevExpress.Mvvm;
using Infrastructure.Connection;
using Infrastructure.MVVM;
using Security.Internal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Workitems.Login.Views
{
    public class LoginViewModel : WorkitemViewModel
    {
        internal AuthenticationController AuthenticationController { get; private set; }

        public LoginViewModel(AuthenticationController authenticationController)
        {
            AuthenticationController = authenticationController;
        }

        private string errorText;

        public string ErrorText
        {
            get { return errorText; }
            set { SetProperty(ref errorText, value, nameof(ErrorText)); }
        }


        private string username = "Admin";

        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value, nameof(Username)); }
        }


        private string password = "_Minibaradmin123";

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, nameof(Password)); }
        }

        private AsyncCommand loginCommand;
        public AsyncCommand LoginCommand =>
            loginCommand ?? (loginCommand = new AsyncCommand(ExecuteLoginCommand, CanExecuteLoginCommand));

        bool IsLoginCommandExecuting;

        async Task ExecuteLoginCommand()
        {
            IsLoginCommandExecuting = true;
            LoginCommand?.RaiseCanExecuteChanged();
            try
            {
                await AuthenticationController.AuthenticateAsync(Username, Password);
                Workitem.Close();
            }
            catch (ApiException apiEx)
            {
                ErrorText = apiEx.Message;
            }

            catch (ApiConnectionException)
            {
                ErrorText = "Couldn't connect to server";
            }
            catch
            {
                ErrorText = "An unkown error occured, please contact your administrator.";
            }
            finally
            {
                IsLoginCommandExecuting = false;
                LoginCommand?.RaiseCanExecuteChanged();

            }
        }

        bool CanExecuteLoginCommand()
        {
            return !IsLoginCommandExecuting;
        }
    }
}
