
using Application;
using Common;
using Common.UI.Method;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;

namespace PublicApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication();
            services.AddInfrastructure(Configuration);

           

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


            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


           


            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseRouting();
          
        
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultCQRSAPI V1");
            //});

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.Run();


            //  app.Run();

        }
    }
}