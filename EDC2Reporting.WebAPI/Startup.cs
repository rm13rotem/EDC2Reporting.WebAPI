using MainStaticMaintainableEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddReact();
            //// Make sure a JS engine is registered, or you will get an error!
            //services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
            //  .AddV8();

            services.AddControllersWithViews();

            if (Configuration.GetValue<string>("Environment") == "DEV")
            {
                services.AddDbContext<ClinicalDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            }
            else if (Configuration.GetValue<string>("Environment") == "TEST")
            {
                services.AddDbContext<ClinicalDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TestConnection")));
            }
            else if (Configuration.GetValue<string>("Environment") == "PREPROD")
            {
                services.AddDbContext<ClinicalDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PreProdConnection")));
            }
            else if (Configuration.GetValue<string>("Environment") == "PROD")
            {
                services.AddDbContext<ClinicalDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("ProdConnection")));
            }
            else
            {
                services.AddDbContext<ClinicalDataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
            }

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
