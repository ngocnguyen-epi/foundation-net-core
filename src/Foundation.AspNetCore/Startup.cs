using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.Data;
using EPiServer.DependencyInjection;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Internal;
using EPiServer.Web.Routing;
using Foundation.AspNetCore.Cms.Settings;
using Foundation.AspNetCore.Extensions;
using Foundation.AspNetCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Foundation.AspNetCore
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionStringCms = _configuration.GetConnectionString("EPiServerDB");
            var connectionStringCommerce = _configuration.GetConnectionString("EcfSqlConnection");

            services.Configure<DataAccessOptions>(o =>
            {
                //o.UpdateDatabaseSchema = true;

                o.SetConnectionString(connectionStringCms);
                o.ConnectionStrings.Add(new ConnectionStringOptions
                {
                    ConnectionString = connectionStringCommerce,
                    Name = "EcfSqlConnection"
                });
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddCmsAspNetIdentity<ApplicationUser>(o =>
            {
                if (string.IsNullOrEmpty(o.ConnectionStringOptions?.ConnectionString))
                {
                    o.ConnectionStringOptions = new ConnectionStringOptions()
                    {
                        Name = "EcfSqlConnection",
                        ConnectionString = connectionStringCommerce
                    };
                }
            });

            services.AddMvc();
            services.AddFoundation();
            services.AddCms();
            services.AddCommerce();
            services.AddTinyMce();

            // Site Specific
            services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
            services.TryAddEnumerable(Microsoft.Extensions.DependencyInjection.ServiceDescriptor.Singleton(typeof(IFirstRequestInitializer), typeof(UsersInstaller)));
            services.AddHttpContextAccessor();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.Configure<MvcOptions>(o =>
            {
                o.ModelBinderProviders.Insert(0, new ModelBinderProvider());
            });

            services.AddEmbeddedLocalization<Startup>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();
                endpoints.MapRazorPages();
            });
        }
    }
}
