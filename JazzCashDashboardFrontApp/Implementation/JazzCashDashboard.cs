using JazzCashDashboardFrontApp.Interfaces.JazzCashGoals;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace JazzCashDashboardFrontApp.Implementation
{
    public class JazzCashDashboard : IJazzCashDashboard
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiBaseUrl;

        #pragma warning disable CS8618, CS8601, CS8603// Possible null reference assignment.

        public JazzCashDashboard(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // _apiBaseUrl = configuration["Endpoint:JAZZ_CASH_APIS_IP:JAZZ_CASH_APIS_PORT_NO"]; // Read API Base URL
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"]; // Read API Base URL

        }

        public async Task<dynamic> GetTotalGoalsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/GetGoalsByDateWise");
            return JsonConvert.DeserializeObject<dynamic>(response);
        }
    }
}
