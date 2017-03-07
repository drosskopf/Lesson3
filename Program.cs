using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace webapp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("hosting.json")
            .Build();
            
            var host = new WebHostBuilder()
               .UseKestrel()
                //.UseUrls("http://localhost:5001/")
               .UseConfiguration(config)
               .UseStartup<Startup>()
               .Build();
            host.Run();
        }
    }
    
    
}
