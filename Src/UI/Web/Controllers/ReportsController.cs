
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Models.Dto;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Security.Cryptography.Xml;

namespace Web.Controllers;

public class ReportsController : Controller
{
    private readonly IHostEnvironment _hostEnvironment;

    public ReportsController( IHostEnvironment hostEnvironment)
    {
       
        _hostEnvironment = hostEnvironment; // has ContentRootPath property

    }


    public IActionResult Index()
    {
        return View();

    }

    public IActionResult GetReport()
    {

        var report = new StiReport();
        report.Load(_hostEnvironment.ContentRootPath + @"\Reports\ReportTest.mrt");
        report.RegBusinessObject("Employee", GetAll());
        return StiNetCoreViewer.GetReportResult(this, report);
    }

    List<EmployeeDto> GetAll()
    {
        return new List<EmployeeDto>() { new EmployeeDto() {  FirstName="test",LastName="bbb" , Position="Test"}
       , new EmployeeDto() {  FirstName="test",LastName="bbb" , Position="Test"}};
    }
    public IActionResult ViewerEvent()
    {
        return StiNetCoreViewer.ViewerEventResult(this);
    }
}
