using CorporatePortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CorporatePortal.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProjectAccounting()
        {
            return View();
        }

        public IActionResult PostingAccounting()
        {
            return View();
        }

        public IActionResult TaskAccounting()
        {
            return View();
        }

        public IActionResult Privacy()
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
