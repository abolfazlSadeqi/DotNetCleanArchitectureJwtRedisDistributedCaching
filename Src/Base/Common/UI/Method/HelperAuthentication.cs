using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UI.Method
{
    public class HelperAuthentication
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                var _result = GEtActionAuthenticationOptions();
                options.DefaultAuthenticateScheme = _result.DefaultAuthenticateScheme;
                options.DefaultChallengeScheme = _result.DefaultChallengeScheme;
                options.DefaultScheme = _result.DefaultScheme;
            })
           .AddJwtBearer(options =>
            {
                var _result = GetJwtBearerOptions(configuration);
                options.IncludeErrorDetails = _result.IncludeErrorDetails;
                options.SaveToken = _result.SaveToken; options.RequireHttpsMetadata = _result.RequireHttpsMetadata;
                options.TokenValidationParameters = _result.TokenValidationParameters;
            });

        }

        static JwtBearerOptions GetJwtBearerOptions(IConfiguration configuration)
        {
            return new JwtBearerOptions()
            {
                IncludeErrorDetails = true,

                SaveToken = true,
                RequireHttpsMetadata = false,
                TokenValidationParameters = GetTokenValidationParameters(configuration),
            };
        }
        static AuthenticationOptions GEtActionAuthenticationOptions()
        {
            return new AuthenticationOptions()
            {

                DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
                DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
                DefaultScheme = JwtBearerDefaults.AuthenticationScheme
            };
        }

        static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,

                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,

                ValidateIssuerSigningKey = true,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidAudience = configuration["Jwt:Audience"],
                ValidIssuer = configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        }
    }
}
