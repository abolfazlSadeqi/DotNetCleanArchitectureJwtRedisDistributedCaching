Implementing CleanArchitecture authentication and authorization in  Web Api by using Identity and JWT and RedisDistributedCaching in in  Web Api  and Logging with elasticsearch in  Web Api   and ReportBusinessObjectStimulSoft in Web and HangFire in Web Mvc

## Tech Specification:

1.authentication and authorization in  Web Api by using Identity and JWT
  
2.RedisDistributedCaching in  Web Api
  
3.Logging with elasticsearch  
  
4.ReportBusinessObjectStimulSoft  in  Web Mvc
  
5.Read And Write Config  in  Web Mvc
  
6.TDD(XUnit)
  
7.BDD (SpecFlow)
  
8.EFCore7 
  
9.Net7
  
10.Swagger UI

## Elasticsearch 
   is a search engine based on the Lucene library. It provides a distributed, multitenant-capable full-text search engine with an HTTP web interface and schema-free JSON documents

## Serilog 
  is a diagnostic logging library for .NET applications. It is easy to set up, has a clean API, and runs on all recent .NET platforms. While it's useful even in the simplest applications, Serilog's support for structured logging shines when instrumenting complex, distributed, and asynchronous 

### Steps implementation (Logging with elasticsearch )

#### 1.Install NuGet Package(s) into your Project

 |PackageName|Desc|
 |---|----|
|Serilog.AspNetCore|Serilog support for ASP.NET Core logging|
|Serilog.Enrichers.Environment| Enrich Serilog log events with properties from System.Environment.|
|Serilog.Exceptions|Serilog.Exceptions is an add-on to Serilog to log exception details and custom properties that are not output in Exception.ToString()|
|Serilog.Sinks.Debug | A Serilog sink that writes log events to the debug output window|
|Serilog.Sinks.Elasticsearch | Serilog sink for Elasticsearch|

#### 2.Adding Serilog in appsettings.json
```
"Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
       "Microsoft":"Information",
        "System": "Warning"
      }
    } },
```

#### 3.Adding ElasticConfiguration  in appsettings.json
```
"ElasticConfiguration": {
    "Uri": "http://localhost:9200"
  },
```

#### 4.Register Serilog or ELK services in  Program.cs or Startup
         services.AddSerilog();
     LoggerSerilogELK.ConfigureLogging(Configuration); 

#### 5.Add Base Class( you need to add the basic code to the project)

#### 6.Register Example logs in your application

Controller
```
private readonly ILogger<CustomersController> _logger;

    public CustomersController(ILogger<CustomersController> logger)
    {
        _logger = logger;
        _DistributedCache = DistributedCache;
    }
```

Action:
```
_logger.LogInformation("start_Customer");
```

## read or update values into appsetting-json in Web Mvc

Example appsetting-json 
```
"AppSettings": {
 "AppVersion": "1.2",
 "AppId": 0,
 "AppName": ""
 },
```

Example of Settings
```
public class AppSettings
 {
 public string AppVersion { get; set; }
 public string TypeLog { get; set; }
 public string AppName { get; set; }
 }
```

### Steps implementation (read  values into appsetting-json )

#### 1.Add configuration to Startup file or Program file:
```
services.AddOptions();
services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
```

#### 2.You could also use below code to gain access to the settings values
```
private readonly AppSettings _settings;
public HomeController(
 IOptionsSnapshot<AppSettings> settings = null)
{
 if (settings != null) _settings = settings.Value;
}
public IActionResult Index()
{
 var _resul = _settings;
 return View();
}
```
### Steps implementation (update values into appsetting-json )

#### 1.Add configuration to Startup file or Program file (For reload Appsetting After Change)
```
public Startup( Microsoft.Extensions.Hosting.IHostingEnvironment env)
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
```

#### 2.Add Base Class(you need to add the basic code to the project)
#### 3.Update AppSetting
```
var settingsUpdater = new AppSettingsUpdater();
settingsUpdater.UpdateAppSetting(Key, value);
```
Example:
```
 public IActionResult Index()
 {
 var settingsUpdater = new AppSettingsUpdater();
 settingsUpdater.UpdateAppSetting("AppSettings:AppVersion", "1.7.5.2");
 settingsUpdater.UpdateAppSetting("AppSettings:AppName", "Test");
 settingsUpdater.UpdateAppSetting("AppSettings:AppId", 1014);
 return View();
 }
 ```

## JWT: 
JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information between parties as a JSON object.
## Identity : 
ASP.NET Core Identity is a membership system which allows you to add login functionality to your application.used to implement forms authentication

More details(About Implementing authentication and authorization in  Web Api by using Identity and JWT)

https://github.com/abolfazlSadeqi/Net7JwtAuthentication/blob/master/README.md

## Defined distributed cache

A distributed cache is a cache shared by multiple app servers, typically maintained as an external service to the app servers that access it. A distributed cache can improve the performance and scalability of an ASP.NET Core app

More details(About Implementing Defined distributed cache  in  Web Api by using Redis)

https://github.com/abolfazlSadeqi/CurdRedisDistributedCaching/blob/main/README.md

# Hangfire
   Hangfire is an open-source framework that  An easy way to perform background processing in .NET and .NET Core applications. No Windows Service or separate process required.
   
More details(About Implementing Hangfire  in  Web Mvc)

https://github.com/abolfazlSadeqi/HangFireNetCoreTutorial/blob/master/README.md

# BusinessObject in StimulSoft

One of the methods of show reports without direct connection to the database is to use Business object 

This is  a very esay simple application ReportBusinessObject to and .Net7 and StimulSoft For Learn

## Defined BusinessObject in StimulSoft
   A Business object is an object of the data class that can be used to represent data in various structures :tables, lists, arrays, etc.  

 More details(about implementation of Use  BusinessObject in Stimulsoft in Web mvc)

 https://github.com/abolfazlSadeqi/ReportBusinessObjectStimulSoft/blob/master/README.md

 


## Clean Architecture

Install Clean.Architecture.Solution.Template on nuget

I used of this is template Clean Architecture([Clean.Architecture.Solution.Template][1] ),customized based on my own code


[1]: https://www.nuget.org/packages/Clean.Architecture.Solution.Template


