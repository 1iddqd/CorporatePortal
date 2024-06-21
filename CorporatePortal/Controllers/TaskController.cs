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

		public async Task<IActionResult> CreateTask()
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
            var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("Tasks", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("TaskAccounting", "Home");
            }
            else
            {
                return View("Error");
            }
		}
    }
}
