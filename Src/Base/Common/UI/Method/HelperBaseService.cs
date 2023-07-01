using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UI.Method;

public class HelperBaseService
{
    public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });

        services.AddControllers();
        services.AddHttpContextAccessor();

        // services.AddControllers();

        services.AddEndpointsApiExplorer();

        //services.AddControllers();
        // services.AddEndpointsApiExplorer();
        //  services.AddSwaggerGen();


        //services.AddSwaggerGen(options =>
        //{
        //    options.SwaggerDoc("v1", new OpenApiInfo
        //    {
        //        Version = "v1",
        //        Title = "CURD",
        //        Description = "Demo API - Abolfazl Sadeghi",
        //    });
        //});

        services.AddControllersWithViews(options =>
                    options.Filters.Add<ApiExceptionFilterAttribute>())
                        .AddFluentValidation(x => x.AutomaticValidationEnabled = false);



    }


}
