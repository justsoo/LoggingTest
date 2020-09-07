using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoggingTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);            

            //Create Autofac Container
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);           

            containerBuilder.RegisterType<UserService>()
                .As<IUserService>()
                .SingleInstance()
                .EnableInterfaceInterceptors();

            containerBuilder.Register(c => new UserServiceInterceptor())
                .Named<IInterceptor>("log-calls");

            var container = containerBuilder.Build();
            //Container inside nozzle
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<UserServiceInterceptor>();
            builder.RegisterType<UserService>().As<IUserService>()
                .EnableInterfaceInterceptors()
                .InstancePerLifetimeScope();
        }
    }
}
