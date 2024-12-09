using MovieReviewer.Data;
using MovieReviewer.Data.Repositories;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Service;
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
builder.Services.AddScoped<IMovieService, MovieService>();

builder.Services.Configure<TmDb>(builder.Configuration.GetSection(nameof(TmDb)));

builder.Services.AddHttpClient("TmDb",
    httpClient =>
    {
        httpClient.BaseAddress = new Uri("https://www.omdbapi.com/");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
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
