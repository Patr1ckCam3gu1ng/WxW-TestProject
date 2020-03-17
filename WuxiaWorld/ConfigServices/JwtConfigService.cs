namespace WuxiaWorld.ConfigServices {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class JwtConfigService {

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfigurationRoot configurationRoot) {

            var tokenParams = configurationRoot.GetSection("Token").GetChildren().ToList();

            var sharedKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(GetValue(tokenParams, "Key")));

            var issuer = GetValue(tokenParams, "Issuer");

            var tokenValidationParameters = new TokenValidationParameters {
                ClockSkew = TimeSpan.FromMinutes(Convert.ToInt16(GetValue(tokenParams, "AddMinutes"))),

                // INFO: Specify the key used to sign the token:
                IssuerSigningKey = sharedKey,
                RequireSignedTokens = true,

                // INFO: Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,

                // INFO: Ensure the token audience matches our audience value (default true):
                ValidateAudience = true,
                ValidAudience = issuer,

                // INFO: Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = true,
                ValidIssuer = issuer
            };

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions => {
                configureOptions.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }

        private static string GetValue(IEnumerable<IConfigurationSection> tokenParams, string key) {

            return tokenParams
                .Where(c => c.Key == key)
                .Select(c => c.Value)
                .FirstOrDefault();
        }
    }

}