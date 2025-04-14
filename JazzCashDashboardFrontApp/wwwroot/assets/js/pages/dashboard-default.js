'use strict';
async function fetchWeeklyCreatedCount() {
    try {
        
        const response = await fetch('/weeklycreatedcount');
        const data = await response.json(); // [5, 8, 10, ...]

        //console.log("Created counts:", data);

        // Example: Loop through and display
        //const container = document.getElementById('weeklyCounts');
        //container.innerHTML = data.map((count, index) => `<li>Week ${index + 1}: ${count}</li>`).join('');
        const _data = data?.filter((i, a) => { return a < 7 && i });
        return _data;

    } catch (err) {
        console.error('Error fetching created counts:', err);
    }
};

document.addEventListener('DOMContentLoaded', function () {
  setTimeout(function () {
    floatchart();
  }, 500);
});

function floatchart() {
  (function () {
    var options = {
      chart: {
        height: 450,
        type: 'area',
        toolbar: {
          show: false
        }
      },
      dataLabels: {
        enabled: false
      },
      colors: ['#1890ff', '#13c2c2'],
      series: [{
        name: 'Page Views',
        data: [31, 40, 28, 51, 42, 109, 100]
      }, {
        name: 'Sessions',
        data: [11, 32, 45, 32, 34, 52, 41]
      }],
      stroke: {
        curve: 'smooth',
        width: 2
      },
      xaxis: {
        categories: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
      }
    };
    var chart = new ApexCharts(document.querySelector('#visitor-chart'), options);
    chart.render();
    var options1 = {
      chart: {
        height: 450,
        type: 'area',
        toolbar: {
          show: false
        }
      },
      dataLabels: {
        enabled: false
      },
      colors: ['#1890ff', '#13c2c2'],
      series: [{
        name: 'Page Views',
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94, 86, 115, 35]
      }, {
        name: 'Sessions',
        data: [110, 60, 150, 35, 60, 36, 26, 45, 65, 52, 53, 41]
      }],
      stroke: {
        curve: 'smooth',
        width: 2
      },
      xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
      }
    };
    var chart = new ApexCharts(document.querySelector('#visitor-chart-1'), options1);
    chart.render();
  })();

    (async function () {
        const _data =await fetchWeeklyCreatedCount()
    var options ={
      chart: {
        type: 'bar',
        height: 365,
        toolbar: {
          show: false
        }
      },
      colors: ['#13c2c2'],
      plotOptions: {
        bar: {
          columnWidth: '45%',
          borderRadius: 4
        }
      },
      dataLabels: {
          //enabled: false
          enabled: true
      },
      series: [{
        //data: [99, 95, 70, 95, 65, 55, 78]
          data: _data
        }],
      stroke: {
        curve: 'smooth',
        width: 2
      },
      xaxis: {
        categories: ['Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa', 'Su'],
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        }
      },
      yaxis: {
        show: false
      },
      grid: {
        show: false
      }
    };
    var chart = new ApexCharts(document.querySelector('#income-overview-chart'), options);
    chart.render();
  })();

  
  (function () {
    var options = {
      chart: {
        type: 'line',
        height: 340,
        toolbar: {
          show: false
        }
      },
      colors: ['#faad14'],
      plotOptions: {
        bar: {
          columnWidth: '45%',
          borderRadius: 4
        }
      },
      stroke: {
        curve: 'smooth',
        width: 1.5
      },
      grid: {
        strokeDashArray: 4
      },
      series: [{
        data: [58, 90, 38, 83, 63, 75, 35, 55]
      }],
      xaxis: {
        type: 'datetime',
        categories: [
          '2018-05-19T00:00:00.000Z',
          '2018-06-19T00:00:00.000Z',
          '2018-07-19T01:30:00.000Z',
          '2018-08-19T02:30:00.000Z',
          '2018-09-19T03:30:00.000Z',
          '2018-10-19T04:30:00.000Z',
          '2018-11-19T05:30:00.000Z',
          '2018-12-19T06:30:00.000Z'
        ],
        labels: {
          format: 'MMM'
        },
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        }
      },
      yaxis: {
        show: false
      },
    };
    var chart = new ApexCharts(document.querySelector('#analytics-report-chart'), options);
    chart.render();
  })();
  (function () {
    var options = {
      chart: {
        type: 'bar',
        height: 430,
        toolbar: {
          show: false
        }
      },
      plotOptions: {
        bar: {
          columnWidth: '30%',
          borderRadius: 4
        }
      },
      stroke: {
        show: true,
        width: 8,
        colors: ['transparent']
      },
      dataLabels: {
        enabled: false
      },
      legend: {
        position: 'top',
        horizontalAlign: 'right',
        show: true,
        fontFamily: `'Public Sans', sans-serif`,
        offsetX: 10,
        offsetY: 10,
        labels: {
          useSeriesColors: false
        },
        markers: {
          width: 10,
          height: 10,
          radius: '50%',
          offsexX: 2,
          offsexY: 2
        },
        itemMargin: {
          horizontal: 15,
          vertical: 5
        }
      },
      colors: ['#faad14', '#1890ff'],
      series: [{
        name: 'Net Profit',
        data: [180, 90, 135, 114, 120, 145]
      }, {
        name: 'Revenue',
        data: [120, 45, 78, 150, 168, 99]
      }],
      xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun']
      },
    }
      var c
      hart = new ApexCharts(document.querySelector('#sales-report-chart'), options);
    chart.render();
  })();
}


function GetCounts() {
    
    $.ajax({
        "async": false,
        "crossDomain": true,
        url: '/GetCount', 
        //type: 'JSON',
        method: 'GET',
        success: function (data) {
            const goalData = JSON.parse(data).result[0];
            $('#activeGoalsCount').text( goalData.TOTAL_ACTIVE_GOAL);
            $('#closedGoalsCount').text(goalData.TOTAL_CLOSED_GOAL);
            $('#expiredGoalsCount').text(goalData.TOTAL_EXPIRED_GOAL);
            $('#completedGoalsCount').text(goalData.TOTAL_COMPLETED_GOAL);
            $('#totalDepositCount').text(goalData.TOTAL_DEPOSIT_COUNT);
            $('#totalWithdrawlCount').text(goalData.TOTAL_WITHDRAWAL_COUNT);
            $('#depositAmount').text("PKR " + goalData.TOTAL_DEPOSIT_AMOUNT);
            $('#withdrawlAmount').text("PKR " + goalData.TOTAL_WITHDRAWAL_AMOUNT);
            $('#closedAmount').text("PKR " + goalData.TOTAL_CLOSED_AMOUNT);
            $('#currentDeposit').text("PKR " + goalData.TOTAL_CURRENT_DEPOSIT);
            $('#certificateCreatedCount').text(goalData.TOTAL_GOAL_CERTIFICATE);
            $('#voucherCreatedCount').text(goalData.TOTAL_GOAL_VOUCHER);
        },
        error: function (xhr, status, error) {
            //console.log(data);
            console.error('Error fetching goal stats:', error);
        }
    });
}
setInterval(function () {
    GetCounts(),
    fetchWeeklyCreatedCount()
}, 5000);

GetCounts();
fetchWeeklyCreatedCount();


