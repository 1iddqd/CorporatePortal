using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public IActionResult CreateEntry()
        {
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
                return View("Error");
            }

        }

        public IActionResult Back()
        {
            return RedirectToAction("PostingAccounting", "Home");
        }
    }
}
