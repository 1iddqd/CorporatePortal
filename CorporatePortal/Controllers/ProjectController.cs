using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CorporatePortal.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;

        public ProjectController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient("MyClient");
        }

        public IActionResult Index()
        {
            return View("CreateProject");
        }

        public IActionResult Back()
        {
            return RedirectToAction("ProjectAccounting", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Models.Project project)
        {
            if(!ModelState.IsValid)
            {
                return View("Error");
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
    }
}
