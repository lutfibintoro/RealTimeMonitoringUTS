using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RealTimeMonitoringUTS.Data;
using RealTimeMonitoringUTS.Models;

namespace RealTimeMonitoringUTS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RealTimeMonitoringDbContext _context;

        public HomeController(ILogger<HomeController> logger, RealTimeMonitoringDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("")]
        public IActionResult Idx()
        {
            return Redirect("/Home/Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Monitor()
        {
            return View();
        }

        public IActionResult History()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
