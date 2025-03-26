using ApplicationHandler.interfaces.JazzCashDashboard;
using JazzCashDashboardFrontApp.Interfaces.JazzCashGoals;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JazzCashDashboardFrontApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboard _dashboard;
     
        public DashboardController(IDashboard dashboardRepository)
        {
            _dashboard = dashboardRepository;
        }

        public IActionResult home()
        {
            return View();
        }

        public async Task<IActionResult> analytics()
        {
            var data = await _dashboard.GetTotalGoalsAsync();

            Console.WriteLine("Controller Data: " + JsonConvert.SerializeObject(data));

            return View(data);
        }
       

        //public async Task<IActionResult> Dashboard()
        //{
        //    var data = await _dashboard.GetTotalGoalsAsync();

        //    Console.WriteLine("Controller Data: " + JsonConvert.SerializeObject(data)); 

        //    return View(data);
        //}
    }
}
