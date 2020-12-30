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
            services.AddControllers();

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

            //services.AddTransient(IRepository<Site>, FromJsonRepository<Site>);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
