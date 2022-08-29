using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using zipExample.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<ZipService>(factory =>
{
    ZipService service = new ZipService();
    return service;
});
builder.Services.AddSingleton<HttpClient>(factory =>
{
    HttpClient service = new HttpClient();
    service.BaseAddress = new Uri("https://localhost:7054");
    return service;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
