using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Api
{
    public static class ApiHelper
    {

        public static void HandleApiException(Exception exception)
        {
            if (exception is ApiException)
                UIHelper.ShowErrorMessageBox(exception.Message);
            else if (exception is ApiConnectionException)
                UIHelper.ShowErrorMessageBox("Couldn't connect to server");
            else
                UIHelper.ShowErrorMessageBox("An unkown error occured, please contact your administrator.");
        }
    }
}
