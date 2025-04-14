using DbHandler.Services;
using ExceptionHandler.Middleware;
using ResponseHandler.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
//builder.Services.AddSharedPackagesServices(builder.Configuration);
builder.Services.AddDbHandlerServices(builder.Configuration);


var app = builder.Build();

//app.UseMiddleware<ExceptionLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//app.UseMiddleware<ResponseHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=analytics}/{id?}");

app.Run();
