using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MovieReviewer.WebUI.Client.Services;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<MovieReviewerApiService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
