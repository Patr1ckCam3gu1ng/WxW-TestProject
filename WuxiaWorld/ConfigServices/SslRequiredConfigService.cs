namespace WuxiaWorld.ConfigServices {

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Rewrite;

    public static class SslRequiredConfigService {

        public static IApplicationBuilder RegisterSslRequired(this IApplicationBuilder applicationBuilder) {

            var options = new RewriteOptions()
                .AddRedirectToHttpsPermanent();

            applicationBuilder.UseRewriter(options);

            return applicationBuilder;
        }
    }

}