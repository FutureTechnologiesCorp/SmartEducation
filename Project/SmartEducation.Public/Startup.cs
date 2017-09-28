using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using SmartEducation.Domain;
using Microsoft.EntityFrameworkCore;
using Core.CQRS;
using SmartEducation.Logic.Public.Test;
using Core.DataAccess;
using Core.Common;

namespace SmartEducation.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TestContext>(opt => opt.UseInMemoryDatabase("Test"));

            services.AddTransient<IExecutor, Executor>();
            services.AddTransient<TestQuery>();
            services.AddTransient<BaseDbContext, TestContext>();

            services.AddTransient<IAmbientContext, AmbientContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
             !Path.HasExtension(context.Request.Path.Value) &&
             !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/build/index.html";
                    await next();
                }
            });
            app.UseMvcWithDefaultRoute();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
