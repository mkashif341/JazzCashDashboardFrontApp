using ApplicationHandler.interfaces.JazzCashDashboard;
using DbHandler.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHandler.Services
{
    public static class DbHandlerServices
    {
        public static IServiceCollection AddDbHandlerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDashboard, Dashboard>();
            return services;
        }
    }
}
