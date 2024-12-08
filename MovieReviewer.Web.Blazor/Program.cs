using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using MovieReviewer.Data;
using MovieReviewer.Data.Repositories;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Web.Blazor.Features;
using MovieReviewer.Web.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddBlazorBootstrap();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieClient, MovieClient>();

builder.Services.Configure<TmDb>(builder.Configuration.GetSection(nameof(TmDb)));
builder.Services.AddHttpClient("TmDb",
    (serviceProvider, httpClient) =>
    {
        var options = serviceProvider.GetRequiredService<IOptions<TmDb>>().Value;
        httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", options.TmDbApiKey );
        
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
