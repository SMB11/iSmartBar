﻿using Flurl.Http;
using PostSharp.Aspects;
using Security.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{

    [Serializable]
    public class ApiExceptionHandlingAttribute : MethodInterceptionAspect
    {
        #region Fields


        #endregion

        public ApiExceptionHandlingAttribute() { }

        #region Public Properties

        #endregion

        #region Public Methods and Operators

        public async override Task OnInvokeAsync(MethodInterceptionArgs args)
        {
            try
            {
                await args.ProceedAsync();
            }
            catch (FlurlHttpException ex)
            {
                await HandleHttpError(ex);
            }
        }
        public async override void OnInvoke(MethodInterceptionArgs args)
        {
            try
            {
                args.Proceed();
            }
            catch (FlurlHttpException ex)
            {
                await HandleHttpError(ex);
            }
        }

        private async Task HandleHttpError(FlurlHttpException ex)
        {
            if(ex.Call.Response == null)
            {
                throw new ApiConnectionException("Couldn't connect to server");
            }
            else if (ex.Call.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized || ex.Call.Response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                throw new ApiException("You are unauthorized to access the resource.", ex.Call.Response.StatusCode);
            }
            else
            {
                ApiError error = await ex.GetResponseJsonAsync<ApiError>();
                throw new ApiException(error.Message, error.Status);
            }
        }

        #endregion
    }
}