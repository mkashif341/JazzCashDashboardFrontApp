﻿using ApplicationHandler.interfaces.JazzCashDashboard;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Text.Json.Nodes;
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

//            for ssl certification error on the development environment

           var handler = new HttpClientHandler
           {
               ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
           };

            _httpClient = new HttpClient(handler);

        }

        public async Task<dynamic> GetTotalGoalsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/GetTotalGoals");
            //var data = JsonConvert.DeserializeObject<dynamic>(response);

            // Extracting first object from "result" array
            //var extractedData = data.result;

            //Console.WriteLine("Extracted Data: " + JsonConvert.SerializeObject(extractedData));

            return response;
        }

        public async Task<dynamic> GetDatewiseGoalsAsync()
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/GetGoalsByDateWise");
            var data = JsonConvert.DeserializeObject<dynamic>(response);
         
            var dataList = data.result;
            return dataList;
        }
        public async Task<dynamic> GetWeeklyCreated_Count()
        {
            var data = GetDatewiseGoalsAsync();
          
           var dataTwo = await GetDatewiseGoalsAsync();
            var createdCounts = new List<decimal>();

            foreach (var item in dataTwo)
            {
                if (item != null && item.CREATED_COUNT != null)
                {
                    decimal createdCount = 0;
                    bool isConverted = decimal.TryParse(item.CREATED_COUNT.ToString(), out createdCount);

                    if (isConverted)
                    {
                        createdCounts.Add(createdCount);
                    }
                }
            }

       
            return createdCounts;
        }



    }
}
