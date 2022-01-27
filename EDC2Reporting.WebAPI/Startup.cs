using DataServices.Providers;
using DataServices.SqlServerRepository;
using MailClientLayer;
using MainStaticMaintainableEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMq;
using System;

namespace EDC2Reporting.WebAPI
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddReact();
            //// Make sure a JS engine is registered, or you will get an error!
            //services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
            //  .AddV8();

            //services.AddSingleton<IConfiguration>(Configuration);

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            var _environment = Configuration.GetValue<string>("Environment");
            if (_environment == "DEV")
            {
                services.AddDbContext<EdcDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            }
            else if (_environment == "TEST")
            {
                services.AddDbContext<EdcDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TestConnection")));
            }
            else if (_environment == "PREPROD")
            {
                services.AddDbContext<EdcDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PreProdConnection")));
            }
            else if (_environment == "PROD")
            {
                services.AddDbContext<EdcDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProdConnection")));
            }
            else
            {
                services.AddDbContext<EdcDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            }

            services.Configure<RepositoryOptions>(Configuration.GetSection(RepositoryOptions.RepositorySettings));
            services.Configure<MailClientOptions>(Configuration.GetSection(MailClientOptions.MailClientSettings));
            services.Configure<RabbitMqOptions>(Configuration.GetSection(RabbitMqOptions.RabbitMqSettings));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Initialise ReactJS.NET. Must be before static files.
            //app.UseReact(config =>
            //{
            // If you want to use server-side rendering of React components,
            // add all the necessary JavaScript files here. This includes
            // your components as well as all of their dependencies.
            // See http://reactjs.net/ for more information. Example:
            //config
            //  .AddScript("~/js/First.jsx")
            //  .AddScript("~/js/Second.jsx");

            // If you use an external build too (for example, Babel, Webpack,
            // Browserify or Gulp), you can improve performance by disabling
            // ReactJS.NET's version of Babel and loading the pre-transpiled
            // scripts. Example:
            //config
            //  .SetLoadBabel(false)
            //  .AddScriptWithoutTransform("~/js/bundle.server.js");
            //});


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "site",
                pattern: "v1/api/site",
                defaults: new { controller = "SiteApi", action = "Index" });

                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
