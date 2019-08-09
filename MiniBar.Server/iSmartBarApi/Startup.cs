using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Configuration;
using Common.Core;
using Common.Enums;
using Common.Filters;
using Common.ResponseHandling;
using CoreAPI.Client;
using Facade.Accessors;
using Facade.Configuration;
using Facade.Managers;
using Facade.Repository;
using FluentValidation.AspNetCore;
using iSmartBar.Repositories.Implementation.Location;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using Managers.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repositories.Implementation.Accessors;
using Repositories.Implementation.Culture;
using Repositories.Implementation.Global;
using Repositories.Implementation.Products;
using Repositories.Implementation.Security;

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

            services.AddSingleton<ResponseProvider>();

            // Add Managers to service collection
            AddManagers(services);

            // Allow Cross Site Access
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddHttpContextAccessor();
            services.AddMvc(opt =>
            {
                // Set up entity fluid validation
                opt.Filters.Add<ValidatorActionFilter>();
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    // Configure JSON serialization and deserialization options
                    // Set Enum Converter
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    // Ignore null values
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                // Add the fuelint validation
                .AddFluentValidation();
            services.AddResponseCaching();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".iSmartBar.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(60);
                options.Cookie.IsEssential = true;
            });

            Mapper.Initialize(cfg => cfg.AddProfiles(typeof(ProfileLocator).Assembly));
            services.AddOptions();
            IConfigurationSection globalOptionsSection = Configuration.GetSection("GlobalOptions");

            GlobalOptions globalOptions = globalOptionsSection.Get<GlobalOptions>();
            services.Configure<GlobalOptions>(globalOptionsSection);
            // Configure Response Options
            services.Configure<ResponseOptions>(options => {
                options.Dictionary = new LocalizationDictionary<object, ApiResponse>();
                options.Dictionary.Add(
                    "en",
                    new Dictionary<object, ApiResponse>
                    {
                        [FaultCode.InvalidInput] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid data"),
                        [FaultCode.InvalidID] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid ID"),
                        [FaultCode.InvalidLimit] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Limit"),
                        [FaultCode.InvalidPage] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Page"),
                        [FaultCode.InvalidUserCredentials] = new FaultResponse(HttpStatusCode.BadRequest, "Invalid Credentials"),
                        [FaultCode.NotAllCulturesProvided] = new FaultResponse(HttpStatusCode.BadRequest, "Data must be provided in all supported languages."),

                    }
                );
            });
            List<string> languages = GetLanguages().Result;
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(languages[0]);
                //By default the below will be set to whatever the server culture is. 
                options.SupportedCultures = languages.Select(l => new System.Globalization.CultureInfo(l)).ToList();
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ServiceProvider>(services.BuildServiceProvider());
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
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICountryInfoRepository, CountryInfoRepository>();
            services.AddTransient<ICountryManager, CountryManager>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICityInfoRepository, CityInfoRepository>();
            services.AddTransient<ICityManager, CityManager>();
            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<IHotelManager, HotelManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseResponseCaching();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());


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
            app.UseSession();
            app.UseMvc();
        }
    }
}
