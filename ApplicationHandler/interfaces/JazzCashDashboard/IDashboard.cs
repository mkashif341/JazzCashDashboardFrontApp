using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationHandler.interfaces.JazzCashDashboard
{
    public interface IDashboard
    {
        Task<dynamic> GetTotalGoalsAsync();
        //  Task<dynamic> GetDatewiseGoalsAsync();
    }
}
