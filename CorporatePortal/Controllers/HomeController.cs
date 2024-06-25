using CorporatePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Diagnostics;
using System.Net.Http;

namespace CorporatePortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings settings;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("MyClient");
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
                List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(data)!;
                _logger.LogInformation("Вывод списка проектов");
                return View(projects);
            }
            _logger.LogError("Ошибка при выводе списка проектов");
            return View("Error");
        }

        public async Task<IActionResult> PostingAccounting()
        {
			var response = await _httpClient.GetAsync("Entries");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(data, settings)!;
                _logger.LogInformation("Вывод списка проходок");
                return View(entries);
			}
            _logger.LogError("Ошибка при выводе списка проходок");
            return View("Error");
		}

        public async Task <IActionResult> TaskAccounting()
        {
			var response = await _httpClient.GetAsync("Tasks");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				List<Models.Task> tasks = JsonConvert.DeserializeObject<List<Models.Task>>(data)!;
                _logger.LogInformation("Вывод списка задач");
                return View(tasks);
			}
            _logger.LogError("Ошибка при выводе списка задач");
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
