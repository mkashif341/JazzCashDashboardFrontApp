//const { type } = require("jquery");

//$.ajax({
//    url: '',
//    //type: 'JSON',
//    method: 'GET',
//    success: function (data) {
//        $('#activeGoalsCount').text(res.TOTAL_ACTIVE_GOAL);
//    },
//    error: function (index, err) { }

//});

//function fetchGoalStats() {
//    debugger;
//    $.ajax({
//        url: '/Dashboard/GetCount',  // Update with your actual route
//        method: 'GET',
//        success: function (data) {
//            $('#activeGoalsCount').text(data.TOTAL_ACTIVE_GOAL);
//            $('#closedGoalsCount').text(data.TOTAL_CLOSED_GOAL);
//            $('#expiredGoalsCount').text(data.TOTAL_EXPIRED_GOAL);
//            $('#completedGoalsCount').text(data.TOTAL_COMPLETED_GOAL);
//            $('#depositAmount').text(data.TOTAL_DEPOSIT_AMOUNT);
//            $('#withdrawlAmount').text(data.TOTAL_WITHDRAWAL_AMOUNT);
//            $('#closedAmount').text(data.TOTAL_CLOSED_AMOUNT);
//            $('#currentDeposit').text(data.TOTAL_CURRENT_DEPOSIT);
//        },
//        error: function (xhr, status, error) {
//            console.error('Error fetching goal stats:', error);
//        }
//    });
//}

//fetchGoalStats();

//// Poll every 5 seconds
//setInterval(fetchGoalStats, 5000);
