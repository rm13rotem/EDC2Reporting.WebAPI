using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace EDC2Reporting.WebAPI
{
    public class Program
    {
        public static void Main(string[] args, int nErrors = 0)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("let's start the App" + DateTime.UtcNow.ToLongDateString());
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error - right on the start of the Program.cs");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging => 
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
            .UseNLog();
    }
}
