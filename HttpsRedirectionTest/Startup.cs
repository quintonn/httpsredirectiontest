using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace HttpsRedirectionTest
{
    /* Scenario 1 */
    /*
     * http://localhost:5000/basic/test1 works as expected
     * http://localhost:5000/basic/test2 is redirected to https://localhost:5001/basic/test2 as expected
     * 
     * Problem: Have to configure SSL port explicitly. Else https redirection redirects to https://localhost/basic/test2 (no port number)
     * */

    /* Scenario 2 */
    /*
     * http://localhost:5000/basic/test1 does not work but is redirected to https://localhost:5001/basic/test1 instead where it does show the correct result
     * http://localhost:5000/basic/test2 is redirected to https://localhost:5001/basic/test2 as expected
     * 
     * Problem: Can't have endpoints without https (i.e. http:// only). in this case /test1
     * 
     * */


    public class Startup
    {
        private static int ScenarioNumber = 1;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); // -->  when i use this, browsing to http://localhost:5020/basic/test2 gives error page.

            /* Scenario 1 */
            if (ScenarioNumber == 1)
            {
                services.AddMvcCore(x =>
                {
                    /* THIS IS MY PROBLEM, I DON'T WANT TO HAVE TO ADD THIS EXPLICITLY */
                    x.SslPort = 5001; // Problem: I have to configure this port here and in Program.cs. Why is this necessary?
                                      // This seems only required when calling app.UseMvc() before calling app.UseHttpsRedirection() below
                });
            }
            if (ScenarioNumber == 2)
            {
                services.AddMvcCore();
            }

            /* Scenario 2 */
            /* This seems to not affect scenario number 1 at all, has no effect */
            if (ScenarioNumber == 2)  // this is required else, http endpoint wont work at all and no http redirection happens
            {
                services.AddHttpsRedirection(x =>
                {
                    x.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                    //x.HttpsPort = 5001; // not needed when default ports are used
                });
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /* Scenario 1 */
            if (ScenarioNumber == 1)
            {
                app.UseMvc(); // setting this before use https redirection allows both http and https endpoints to work, but redirect to https requires SslPort above
            }

            app.UseHttpsRedirection();

            /* Scenario 2 */
            if (ScenarioNumber == 2)
            {
                app.UseMvc(); // set
            }
        }
    }
}
