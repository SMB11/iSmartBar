using BusinessEntities.Security;
using Common.ResponseHandling;
using Common.Filters;
using AutoMapper;
using FluentValidation.AspNetCore;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using LinqToDB.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using Common.Configuration;
using Common.Core;
using System.Collections.Generic;
using Common.Enums;
using System.Net;
using Facade.Managers;
using Facade.Repository;
using Repositories.Implementation;
using Managers.Implementation;
using Repositories.Implementation.Global;
using Repositories.Implementation.Security;
using Repositories.Implementation.Culture;
using System.Linq;
using Repositories.Implementation.Products;
using Facade.Accessors;
using Repositories.Implementation.Accessors;
using Facade.Configuration;
using Core.Repositories.Implementation;

namespace CoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Linq2DB Config

            // Set Linq2DB Connection String
            DataConnection
                .AddConfiguration(
                    "Default",
                    Configuration["ConnectionString"],
                    new SqlServerDataProvider("Default", SqlServerVersion.v2012));

            DataConnection.DefaultConfiguration = "Default";

            // Configure Linq2DB Identity
            services.AddIdentity<User, LinqToDB.Identity.IdentityRole>()
                .AddLinqToDBStores(new DefaultConnectionFactory())
                .AddDefaultTokenProviders();
            #endregion

            #region Authentication Configuration

            // Configure Jwt Security
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Interop";
                    options.DataProtectionProvider =
                        DataProtectionProvider.Create(new DirectoryInfo("C:\\Github\\Identity\\artifacts"));

                });
            
            
            #endregion

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
            services.AddMemoryCache();
            //services.AddAutoMapper();

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
            IList<string> languages = new LanguageRepository().GetAll().Select(l => l.ID).ToList();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(languages[0]);
                //By default the below will be set to whatever the server culture is. 
                options.SupportedCultures = languages.Select(l => new System.Globalization.CultureInfo(l)).ToList();
            });
            services.AddSingleton<ResponseProvider>();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ServiceProvider>(services.BuildServiceProvider());
        }
        
        private void AddManagers(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationManager, Managers.Implementation.AuthenticationManager>();
            services.AddTransient<IAssetRepository, AssetRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRolesManager, UserRolesManager>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<ILanguageManager, LanguageManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryNameRepository, CategoryNameRepository>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IBrandAccessor, BrandAccessor>();
            services.AddTransient<IBrandManager, BrandManager>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductInfoRepository, ProductInfoRepository>();
            services.AddTransient<IImageManager, ImageManager>();
            services.AddTransient<IImageRepository, ImageRepository>();
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

            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
