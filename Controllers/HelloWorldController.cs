using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealTimeMonitoringUTS.Models;

namespace RealTimeMonitoringUTS.Controllers
{
    public class HelloWorldController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult<string> Welcome([FromQuery] string name, [FromRoute] int id)
        {
            ViewData["NumTimes"] = id;
            ViewData["Message"] = "hello: " + name;
            return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
