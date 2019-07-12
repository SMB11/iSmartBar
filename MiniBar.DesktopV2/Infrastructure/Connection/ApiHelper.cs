using Infrastructure.Util;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Connection
{
    public static class ApiHelper
    {

        public static void HandleApiException(Exception exception, string defaultMessage = null, Action afterHandle = null)
        {
            if (exception is TaskCanceledException || exception.InnerException is TaskCanceledException)
                return;
            else
            {
                if (exception is ApiException)
                    UIHelper.Error(exception.Message);
                else if (exception is ApiConnectionException)
                    UIHelper.Error("Couldn't connect to server");
                else if(String.IsNullOrEmpty(defaultMessage))
                    UIHelper.Error("An unkown error occured, please contact your administrator.");
                else
                    UIHelper.Error(defaultMessage);
                afterHandle?.Invoke();
            }
            
        }
    }
}
