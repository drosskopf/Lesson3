using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace webapp
{
   public class Startup{
        public IConfigurationRoot Configuration {get;}
        public Startup(IHostingEnvironment env)
        {
            //Environment Development, Staging, Production
            //Powershell $env:ASPNETCORE_ENVIRONMENT="Development"
            //IOS EXPORT ASPNETCORE_ENVIRONMENT="Development"
            //CMD Setx ASPNETCORE_ENVIRONMENT="Development"
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings_{env.EnvironmentName}.json",true);
            Configuration = config.Build();
            
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<Family>(Configuration.GetSection("Family"));
        }
        public void Configure(IApplicationBuilder app, IOptions<Family> family)
        {
            var dad = family.Value.Dad;
                   app.Run(async (context) =>
                    { 
                        var person = context.Request.Path.Value.Replace("/","");
                        await context.Response.WriteAsync($"<h1>Hello World {dad}</h1>");
                    });
        }
    }
}