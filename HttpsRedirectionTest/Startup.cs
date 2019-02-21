using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace HttpsRedirectionTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(); // -->  when i use this, browsing to http://localhost:5020/basic/test2 gives error page.

            /* This works */
            services.AddMvcCore(x =>
            {
                x.SslPort = 5021; // Problem: I have to configure this port here and in Program.cs. Why is this necessary?
            });

            services.AddHttpContextAccessor();
            services.AddHttpsRedirection(x =>
            {
                x.HttpsPort = 5022; // this is not used, has no effect at all.
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseHttpsRedirection();
            
        }
    }
}
