using CorporatePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CorporatePortal.Controllers
{
	public class TaskController : Controller
	{

        private readonly ILogger<TaskController> _logger;
        private readonly HttpClient _httpClient;
        public TaskController(ILogger<TaskController> logger, IHttpClientFactory httpClientFactory) 
		{
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("MyClient");
        }

		public IActionResult CreateTask()
		{
            return View();
        }

        public IActionResult Back()
        {
            return RedirectToAction("TaskAccounting", "Home");
        }

		[HttpPost]
		public async Task<IActionResult> CreateTask(CorporatePortal.Models.Task task)
		{

            if(task.ProjectId <= 0)
            {
                return View();
            }
			else
			{
				var project = new Project()
				{
					Id = task.ProjectId,
					Name = "1",
					Code = "1",
					IsActive = true,
				};
				task.Project = project;
			}
			var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseTask = await _httpClient.PostAsync("Tasks", content);

            if (responseTask.IsSuccessStatusCode)
            {
                return RedirectToAction("TaskAccounting", "Home");
            }
            else
            {
                return View("Error");
            }
		}
		public async Task<IActionResult> EditTask(int id)
		{
			var response = await _httpClient.GetAsync($"Tasks/{id}");
			if (response.IsSuccessStatusCode)
			{
				string data = await response.Content.ReadAsStringAsync();
				var task = JsonConvert.DeserializeObject<Models.Task>(data);
				return View(task);
			}
			return View("Error");
		}

		[HttpPost]
		public async Task<IActionResult> EditTask(Models.Task task)
		{
			if (task.ProjectId <= 0)
			{
				return View();
			}
			else
			{
				var project = new Project()
				{
					Id = task.ProjectId,
					Name = "1",
					Code = "1",
					IsActive = true,
				};
				task.Project = project;
			}

			var json = JsonConvert.SerializeObject(task);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await _httpClient.PutAsync($"Tasks/{task.Id}", content);

			if (response.IsSuccessStatusCode)
			{
				return RedirectToAction("TaskAccounting", "Home");
			}
			else
			{
				return View("Error");
			}
		}

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var response = await _httpClient.GetAsync($"Tasks/{id}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var project = JsonConvert.DeserializeObject<Models.Task>(data);
                return View(project);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTaskConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Tasks/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TaskAccounting", "Home");
            }

            return View("Error");

        }
    }
}
