using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               .Build();
            host.Run();
        }
        public class Startup{
            //using Microsoft.Extensions.DependencyInjection;
            public IConfigurationRoot Configuration {get;}
            public Startup(IHostingEnvironment env)
            {
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings-{env.EnvironmentName}.json",true);
                Configuration = config.Build();
            }
            public void Configure(IApplicationBuilder app, IOptions<Family> family)
            {
                app.Run(async (context) =>
                    { 
                        var person = context.Request.Path.Value.Replace("/","");
                        await context.Response.WriteAsync($"<h1>Hello World {person} I am  your Father {family.Value.Dad} </h1>");
                    });
            }
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddOptions();
                //Add "Microsoft.Extensions.Options.ConfigurationExtensions": "1.0.0"
                services.Configure<Family>(Configuration.GetSection("Family"));
            }

        }
        public class Family
        {
            public string Dad {get;set;}
            public string Mom {get;set;}
        }
    }
}
