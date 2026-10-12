using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Homeocentrum.Niga.OldAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // This API has one appsettings.json. Do not load appsettings.Development.json or any other environment file.
                    config.Sources.Clear();
                    config.SetBasePath(context.HostingEnvironment.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
                })
                // Nothing goes to the console. Logs are written by AppFileLoggerProvider (Startup) under Logs/.
                .ConfigureLogging(logging => logging.ClearProviders())
                .SuppressStatusMessages(true)
                .ConfigureKestrel(options => options.AddServerHeader = false)
                .UseStartup<Startup>();
    }
}
