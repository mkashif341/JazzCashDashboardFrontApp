using ApplicationHandler.interfaces.JazzCashDashboard;
using JazzCashDashboardFrontApp.Interfaces.JazzCashGoals;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace JazzCashDashboardFrontApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;
        private readonly HttpClient _httpClient;


        public DashboardController(IDashboard dashboardRepository, IHttpClientFactory httpClientFactory)
        {
            _dashboard = dashboardRepository;
            _httpClient = httpClientFactory.CreateClient();
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);

        }

        public IActionResult home()
        {
            return View();
        }

        public async Task<IActionResult> analytics()
        {
            var data = await _dashboard.GetTotalGoalsAsync();

            Console.WriteLine("Controller Data: " + JsonConvert.SerializeObject(data));

            //var response = await _httpClient.GetStringAsync("https://localhost:7074/api/JazzCashGoals/GetTotalGoals");
            // var data = JsonConvert.DeserializeObject<dynamic>(response);

            return View(data);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var response = await _httpClient.GetStringAsync("https://yourapi.com/api/dashboard");
        //    var data = JsonConvert.DeserializeObject<dynamic>(response);
        //    return View(data);
        //}

        //public async Task<IActionResult> Dashboard()
        //{
        //    var data = await _dashboard.GetTotalGoalsAsync();

        //    Console.WriteLine("Controller Data: " + JsonConvert.SerializeObject(data)); 

        //    return View(data);
        //}
    }
}
