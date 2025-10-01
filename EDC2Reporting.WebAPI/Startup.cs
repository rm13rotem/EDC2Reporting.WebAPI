using DataServices.Providers;
using DataServices.SqlServerRepository;
using DataServices.SqlServerRepository.Models;
using Edc2Reporting.AuthenticationStartup.Areas.Identity;
using EDC2Reporting.WebAPI.Filters;
using EDC2Reporting.WebAPI.Models.Managers;
using MailClientLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMq;
using SessionLayer;
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
            services.AddControllersWithViews(options => {
                options.Filters.Add<LogExceptionsFilter>();
                options.Filters.Add<LogUserActivityFilter>();
            }).AddRazorRuntimeCompilation();

            services.AddControllers();

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<Investigator, IdentityRole>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

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

            services.AddHttpContextAccessor();
            services.AddScoped<UserManager<Investigator>>();

            services.Configure<RepositoryOptions>(Configuration.GetSection(RepositoryOptions.RepositorySettings));
            services.Configure<MailClientOptions>(Configuration.GetSection(MailClientOptions.MailClientSettings));
            services.Configure<RabbitMqOptions>(Configuration.GetSection(RabbitMqOptions.RabbitMqSettings));

            services.AddScoped<ICrfPageManager, CrfPageManager>();
            services.AddScoped<ISessionWrapper, SessionWrapper>();

            services.AddScoped<IMailClientSender, MailClientSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");


                // Enables attribute routing (like [Route("api/...")])
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

        }

   }
}
