using Microsoft.Extensions.DependencyInjection;

using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;

namespace Common.UI.Method
{

    public class HelperSwagger
    {
        public static void ConfigureService(IServiceCollection services)
        {

           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", GetOpenApiInfo());
                c.AddSecurityDefinition("Bearer", GetOpenApiSecurityScheme());
                c.AddSecurityRequirement(GetOpenApiSecurityRequirement());
            });

        }


        private static OpenApiInfo GetOpenApiInfo()
        {
            return new OpenApiInfo()
            {
                Title = "JWTToken_API",
                Version = "v1",
                Description = "Demo API - Abolfazl Sadeghi"
            };
        }


        private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
        {
          return new OpenApiSecurityScheme()
          {
              Name = "Authorization",
              Type = SecuritySchemeType.Http,
              Scheme = "Bearer",
              BearerFormat = "JWT",
              In = ParameterLocation.Header,
              Description = "JWT Authorization header using the Bearer scheme. Enter your token ",
          };
        }


        private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
        {
            var _Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" };

            var _OpenApiSecurityScheme = new OpenApiSecurityScheme
            {
                Reference = _Reference 
            };

            return new OpenApiSecurityRequirement() { { _OpenApiSecurityScheme, new List<string>() } };


        }
    }

}
