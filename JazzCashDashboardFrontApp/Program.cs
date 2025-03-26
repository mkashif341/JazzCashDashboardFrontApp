using DbHandler.Services;
using ExceptionHandler.Middleware;
using JazzCashDashboardFrontApp.Implementation;
using JazzCashDashboardFrontApp.Interfaces.JazzCashGoals;
using ResponseHandler.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IJazzCashDashboard,JazzCashDashboard>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
//builder.Services.AddSharedPackagesServices(builder.Configuration);
builder.Services.AddDbHandlerServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseMiddleware<ResponseHandlingMiddleware>();

app.UseMiddleware<ExceptionLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=analytics}/{id?}");

app.Run();
