using CorporatePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CorporatePortal.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private HttpClient _httpClient = new HttpClient()
        {
			BaseAddress = new Uri("http://localhost:5125/api/")
	    };

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			return View();
        }

        public async Task<IActionResult> ProjectAccounting()
        {
		    var response = await _httpClient.GetAsync("Projects");
            if(response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(data);
                return View(projects);
            }
            return View("Error");
        }

        public async Task<IActionResult> PostingAccounting()
        {
			var response = await _httpClient.GetAsync("Entries");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(data);
				return View(entries);
			}
			return View("Error");
		}

        public async Task <IActionResult> TaskAccounting()
        {
			var response = await _httpClient.GetAsync("Tasks");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				List<Models.Task> tasks = JsonConvert.DeserializeObject<List<Models.Task>>(data);
				return View(tasks);
			}
			return View("Error");
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
