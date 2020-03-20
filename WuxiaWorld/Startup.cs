namespace WuxiaWorld {

    using ConfigServices;

    using DAL.Entities;

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
            services.RegisterAppSetting(_configurationRoot);
            services.RegisterRdsDatabaseContext(_configurationRoot);

            services.AddMemoryCache();
            services.AddOptions();
            services.AddJwtAuthentication(_configurationRoot);
            services.AddHttpContextAccessor();

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp";
            });

            services.AddCors(options => {
                options.AddPolicy("AllowAllHeaders",
                    builder => {
                        builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WuxiaWorldDbContext dataContext) {

            dataContext.Database.Migrate();

            if (env.IsDevelopment()) {

                app.UseCors("AllowAllHeaders");

                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //INFO: Customize ApplicationBuilder
            app.RegisterSslRequired();
            app.RegisterMvcRouting();

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
    }

}