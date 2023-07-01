
using Hangfire;
using Common.Hangfire;
using Models.Setting;

namespace Web;

public class Startup
{
    //public Startup(IConfiguration configuration)
    //{
    //    Configuration = configuration;
    //}

    public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment env)
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false,
       reloadOnChange: true)
        .AddEnvironmentVariables();
        Configuration = builder.Build();
        //Configuration = configuration;
    }


    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {


        services.AddControllers();
        services.AddHttpContextAccessor();



        services.AddControllers();
        
        services.AddControllersWithViews();
        services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
        services.AddHangfireServer();

        services.AddOptions();
        services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }


        //app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

       // app.UseHangfireDashboard();

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangFireNetCoreTutorialAuthorizationFilter() }
        });


        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllerRoute(
        //      name: "default",
        //       pattern: "{controller=Reports}/{action=Index}/{id?}");
        //});
        app.UseStaticFiles();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
             name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");

            // HangFire Dashboard endpoint
            endpoints.MapHangfireDashboard();
        });

    }
}
