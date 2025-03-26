using ApplicationHandler.interfaces.JazzCashDashboard;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Implementations
{
    public class Dashboard : IDashboard
    {
        private readonly HttpClient _httpClient;

        private readonly string _apiBaseUrl;

#pragma warning disable CS8618, CS8601, CS8603// Possible null reference assignment.

        public Dashboard(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"];  

            // for ssl certification error on the development environment
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler);

        }

     
        public async Task<dynamic> GetTotalGoalsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/GetTotalGoals");
            var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Extracting first object from "result" array
            var extractedData = data.result[0];

            Console.WriteLine("Extracted Data: " + JsonConvert.SerializeObject(extractedData)); 

            return extractedData;
        }
   
    }
}
