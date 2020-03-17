namespace WuxiaWorld.ConfigServices {

    using Microsoft.AspNetCore.Builder;

    public static class MvcRouteConfigService {
        public static IApplicationBuilder RegisterMvcRouting(this IApplicationBuilder app) {

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }
    }

}