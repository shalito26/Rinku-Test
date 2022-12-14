using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LeadsServices
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appseting.json", true)
                .Build();

            CreateWebHostBuilder(args, config).Build().Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration config)
        {
            return WebHost.CreateDefaultBuilder(args).UseConfiguration(config).UseKestrel(opt =>
            {
                opt.Limits.MinRequestBodyDataRate = null;

            }).UseIIS().UseStartup<Startup>();
        }


    }
}
