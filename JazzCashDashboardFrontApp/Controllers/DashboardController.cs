using ApplicationHandler.interfaces.JazzCashDashboard;
using EntityHandler.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Dynamic;
using System.Net.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //public async Task<IActionResult> analytics()
        //{

        //    var model = new MyCombinedViewModel
        //    {
        //        DataOne = await _dashboard.GetTotalGoalsAsync(),
        //        DataTwo = await _dashboard.GetWeeklyCreated_Count()
        //    };

        //    return View(model);
        //}

        public async Task<IActionResult> analytics()
        {


            var DataOne = await GetCount();
                //DataTwo = await _dashboard.GetWeeklyCreated_Count()
            

            return View();
        }


        [HttpGet("weeklycreatedcount")]
        public async Task<IActionResult> GetWeeklyCreatedCount()
        {
            var createdCounts = await _dashboard.GetWeeklyCreated_Count();
            return Ok(createdCounts);
        }

        [HttpGet("GetCount")]
        public async Task<IActionResult> GetCount()
        {
            var createdCounts = await _dashboard.GetTotalGoalsAsync();
            return Ok(createdCounts);
        }
    }
}
