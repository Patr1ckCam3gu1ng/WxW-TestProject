namespace WuxiaWorld.ConfigServices {

    using Microsoft.AspNetCore.Builder;

    public static class MvcRouteConfigService {
        public static IApplicationBuilder RegisterMvcRouting(this IApplicationBuilder app) {

            app.UseRouting();

            app.UseEndpoints(endpoints => {

                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action}/{id?}",
                    new {controller = "ApplicationNew", action = "Default"});
            });

            return app;
        }
    }

}