
using Application;
using Common;
using Common.HelperLog;
using Common.UI.Method;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace PublicApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }



    public void ConfigureServices(IServiceCollection services)
    {
        LoggerSerilogELK.ConfigureLogging(Configuration);

        services.AddApplication();
        services.AddInfrastructure(Configuration);

        services.AddSerilog();

        HelperBaseService.ConfigureService(services, Configuration);

        HelperAuthentication.ConfigureService(services, Configuration);

        HelperSwagger.ConfigureService(services);


        //services.AddControllersWithViews(options =>
        //            options.Filters.Add<ApiExceptionFilterAttribute>())
        //                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {


        // Seed Data
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var scopeProvider = scope.ServiceProvider;
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //  await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
        }

       
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }





        app.UseStaticFiles();

        app.UseHttpsRedirection();


        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();




        //app.UseSwaggerUI(c =>
        //{
        //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultCQRSAPI V1");
        //});

       // app.UseSerilogRequestLogging();


        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        //app.Run();


        //  app.Run();

    }



}