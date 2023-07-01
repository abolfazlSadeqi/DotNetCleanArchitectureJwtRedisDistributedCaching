using Common.UI.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models.Setting;

namespace Web.Controllers;

public class AppSettingController : Controller
{

    private readonly AppSettings _settings;
    Microsoft.Extensions.Hosting.IHostingEnvironment env;
    public AppSettingController(
     Microsoft.Extensions.Hosting.IHostingEnvironment _env, IOptionsSnapshot<AppSettings> settings = null)
    {
        if (settings != null) _settings = settings.Value;
        env = _env;
    }

    
   
    public IActionResult Index()
    {
       // var _resul = _settings;
        return View(_settings);
    }

    [HttpPost]
    public IActionResult Edit(AppSettings appSettings)
    {
        var settingsUpdater = new AppSettingsUpdater(env);
        settingsUpdater.UpdateAppSetting("AppSettings:AppVersion", appSettings.AppVersion);
        settingsUpdater.UpdateAppSetting("AppSettings:AppName", appSettings.AppName);
        settingsUpdater.UpdateAppSetting("AppSettings:AppId", appSettings.AppId);

        return RedirectToAction("Index");
    }

}
