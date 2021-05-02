using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;

namespace MOF.Etimad.Monafasat.Web
{
   public class Program
   {
      public static void Main(string[] args)
      {
         // NLog: setup the logger first to catch all errors
         var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
         try
         {
            logger.Debug("init main.");
            CreateWebHostBuilder(args).Build().Run();
         }
         catch (Exception ex)
         {
            //NLog: catch setup errors
            logger.Error(ex, "Error in loading nlog.config");
         }
         finally
         {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            NLog.LogManager.Shutdown();
         }
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      {
         return WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .ConfigureKestrel(options =>
              {
                 options.AddServerHeader = false;
              })
           .ConfigureLogging(logging =>
              {
                 logging.ClearProviders();
                 logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                 if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                 {
                    logging.AddConsole();
                    logging.AddDebug();
                 }
              })
              .UseNLog();
      }

   }
}
