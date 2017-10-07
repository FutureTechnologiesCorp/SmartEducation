using Core.IdentityServer.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddTestUsers(IdentityConfig.GetTestUsers())
                    .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                    .AddInMemoryClients(IdentityConfig.GetClients());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
        }
    }
}
