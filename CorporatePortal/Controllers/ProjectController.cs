using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CorporatePortal.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<ProjectController> _logger;
        private readonly HttpClient _httpClient;

        public ProjectController(ILogger<ProjectController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("MyClient");
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        public IActionResult Back()
        {
            return RedirectToAction("ProjectAccounting", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(Models.Project project)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogError("Ошибка валидации");
                return View("CreateProject");
            }

            var json = JsonConvert.SerializeObject(project);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("Projects", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProjectAccounting", "Home");
            }
            else
            {
                return View("Error");
            }
            
        }

        public async Task<IActionResult> EditProject(int id)
        {
			var response = await _httpClient.GetAsync($"Projects/{id}");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				var project = JsonConvert.DeserializeObject<Models.Project>(data);
				return View(project);
			}
            return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> EditProject(Models.Project project)
		{
			if (!ModelState.IsValid)
			{
                return RedirectToAction("EditProject");
			}

			var json = JsonConvert.SerializeObject(project);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _httpClient.PutAsync($"Projects/{project.Id}", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("ProjectAccounting", "Home");
			}
			else
			{
				return View("Error");
			}
		}

        [HttpGet]
        public async Task<IActionResult> DeleteProject(int id)
        {
			var response = await _httpClient.GetAsync($"Projects/{id}");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				var project = JsonConvert.DeserializeObject<Models.Project>(data);
				return View(project);
			}
			return View("Error");
		}

        [HttpPost]
        public async Task<IActionResult> DeleteProjectConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Projects/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProjectAccounting", "Home");
            }

            return View("Error");

        }
        
	}
}
