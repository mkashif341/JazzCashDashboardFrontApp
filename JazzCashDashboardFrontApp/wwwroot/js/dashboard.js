//var connection = new signalR.HubConnectionBuilder()
//    .withUrl("https://localhost:7074/api/JazzCashGoals/notify")
//    .build();

//connection.start().then(() => {
//    console.log("Connected to SignalR Hub");
//}).catch(err => console.error(err.toString()));

//connection.on("ReceiveUpdate", message => {
//    console.log("New Update:", message);
//    location.reload(); // Reload dashboard
//});

var connection = new signalR.HubConnectionBuilder()
   // .withUrl("https://localhost:7074/dashboardHub", {
    .withUrl("https://localhost:7074/api/JazzCashGoals/notify", {
        withCredentials: true // Ensure CORS credentials are included
    })
    .build();

connection.start().then(() => {
    console.log("Connected to SignalR Hub");
}).catch(err => console.error(err.toString()));

connection.on("ReceiveUpdate", message => {
    console.log("New Update:", message);
    location.reload(); // Reload dashboard
});
