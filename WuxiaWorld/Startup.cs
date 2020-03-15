namespace WuxiaWorld {

    using ConfigServices;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
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

            services.RegisterMvc();
            services.RegisterDInjection();
            services.RegisterMapper();
            services.RegisterJsonFiles(_configurationRoot);

            services.AddOptions();
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {

                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseResponseCompression();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

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