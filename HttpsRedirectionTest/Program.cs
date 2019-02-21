using Microsoft.AspNetCore.Hosting;

namespace HttpsRedirectionTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                  .UseUrls("https://localhost:5021", "http://localhost:5020")
                  .UseStartup<Startup>()
                  .UseKestrel()
                  .Build()
                  .Run();
        }
    }
}
