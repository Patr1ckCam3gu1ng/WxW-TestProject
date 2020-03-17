namespace WuxiaWorld {

    using System.Linq;

    using ConfigServices;

    using DAL.Entities;
    using DAL.Models;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup {

        private readonly IConfigurationRoot _configurationRoot;

        public Startup() {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true);

            _configurationRoot = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {

            //INFO: Customize ServiceCollection
            services.RegisterMvc();
            services.RegisterDInjection();
            services.RegisterMapper();
            services.RegisterJsonFiles(_configurationRoot);
            services.RegisterRdsDatabaseContext(_configurationRoot);

            services.AddOptions();
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddJwtAuthentication(_configurationRoot.GetSection("Token").GetChildren().ToList());
            services.Configure<CancelToken>(_configurationRoot.GetSection("CancelToken"));
            services.Configure<JwtToken>(_configurationRoot.GetSection("Token"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WuxiaWorldDbContext dataContext) {

            dataContext.Database.Migrate();

            if (env.IsDevelopment()) {

                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //INFO: Customize ApplicationBuilder
            app.RegisterSslRequired();
            app.RegisterMvcRouting();

            // app.UseSpa(spa => {
            //     spa.Options.SourcePath = "ClientApp";
            //
            //     if (env.IsDevelopment()) {
            //         spa.UseReactDevelopmentServer("start");
            //     }
            // });
        }
    }

}