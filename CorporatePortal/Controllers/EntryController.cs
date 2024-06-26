using CorporatePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Text;

namespace CorporatePortal.Controllers
{
    public class EntryController : Controller
    {
        private readonly ILogger<EntryController> _logger;
        private readonly HttpClient _httpClient;

        public EntryController(ILogger<EntryController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("MyClient");
        }

        public async Task<IActionResult> CreateEntry()
        {
            var tasks = await GetTasksFromApi();
            ViewBag.Tasks = new SelectList(tasks, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry(Models.Entry entry)
        {
            if (entry.TaskId <= 0)
            {
                return View();
            }
            else
            {
                var project = new Models.Project()
                {
                    Id = 1,
                    Code = "1",
                    IsActive = true, 
                    Name = "1",
                };
                var task = new Models.Task()
                {
                    Id = entry.TaskId,
                    Name = "1",
                    IsActive = true,
                    ProjectId = 1,
                    Project = project,
                };
                entry.Task = task;
            }

            var json = JsonConvert.SerializeObject(entry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("Entries", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PostingAccounting", "Home");
            }
            else
            {
                var error = new ErrorViewModel()
                {
                    RequestContent = await response.Content.ReadAsStringAsync()
                };
                return View("Error", error);
            }

        }

        public async Task<IActionResult> EditEntry(int id)
        {
            List<Models.Task> tasks;
            var responseTask = await _httpClient.GetAsync("Tasks");
            string dataTask = await responseTask.Content.ReadAsStringAsync();
            tasks = JsonConvert.DeserializeObject<List<Models.Task>>(dataTask)!;

            if (tasks == null || !tasks.Any())
            {
                return View("Error");
            }
            ViewBag.Tasks = new SelectList(tasks, "Id", "Name");
            var responseEntry = await _httpClient.GetAsync($"Entries/{id}");
            if (responseEntry.IsSuccessStatusCode)
            {
                string dataEntry = await responseEntry.Content.ReadAsStringAsync();
                var entry = JsonConvert.DeserializeObject<Models.Entry>(dataEntry);
                return View(entry);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> EditEntry(Models.Entry entry)
        {
            var project = new Models.Project()
            {
                Id = 1,
                Code = "1",
                IsActive = true,
                Name = "1",
            };
            var task = new Models.Task()
            {
                Id = entry.TaskId,
                Name = "1",
                IsActive = true,
                ProjectId = 1,
                Project = project,
            };
            entry.Task = task;

            var json = JsonConvert.SerializeObject(entry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync($"Entries/{entry.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("PostingAccounting", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        public IActionResult Back()
        {
            return RedirectToAction("PostingAccounting", "Home");
        }

        private async Task<IEnumerable<Models.Task>> GetTasksFromApi()
        {
            var response = await _httpClient.GetAsync("Tasks/ActiveTasks");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Models.Task> tasks = JsonConvert.DeserializeObject<List<Models.Task>>(data);
                return tasks ?? new List<Models.Task>();
            }

            return new List<Models.Task>();
        }
    }
}
