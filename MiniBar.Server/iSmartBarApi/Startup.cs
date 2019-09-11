using AutoMapper;
using Common.Configuration;
using Common.Core;
using Common.Enums;
using Common.Filters;
using Common.Middleware;
using Common.ResponseHandling;
using Common.Services;
using CoreAPI.Client;
using Facade.Configuration;
using Facade.Managers;
using Facade.Repository;
using FluentValidation.AspNetCore;
using Hubs;
using iSmartBar.Repositories.Implementation.Location;
using iSmartBar.Repositories.Implementation.Statistics;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Managers.Implementation;
using Managers.Notifications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace iSmartBarApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public async void ConfigureServices(IServiceCollection services)
        {
            #region Linq2DB Config

            // Set Linq2DB Connection String
            DataConnection
                .AddConfiguration(
                    "Default",
                    Configuration["ConnectionString"],
                    new SqlServerDataProvider("Default", SqlServerVersion.v2012));

            DataConnection.DefaultConfiguration = "Default";

            #endregion

        }

        private async Task<List<string>> GetLanguages()
        {
            try
            {
                var langs = await CoreAPIClient.GetLanguages();
                return langs.Select(l => l.ID).ToList();
            }
            catch
            {
                Thread.Sleep(2000);

                var langs = await CoreAPIClient.GetLanguages();
                return langs.Select(l => l.ID).ToList();
            }
        }

        private void AddManagers(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseErrorHandling();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
