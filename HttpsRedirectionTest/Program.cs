using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace HttpsRedirectionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                  .UseUrls("https://localhost:5021", "http://localhost:5020")
                  .ConfigureLogging((hostingContext, logging) =>
                  {
                      logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                      logging.AddConsole();
                      logging.AddDebug();
                      logging.AddEventSourceLogger();
                  })
                  .UseStartup<Startup>()
                  .UseKestrel()

                  .Build()
                  .Run();
        }
    }
}
