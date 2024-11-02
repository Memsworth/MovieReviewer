using MovieReviewer.Core.Infrastructure;
using MovieReviewer.Core.Infrastructure.Repositories;
using MovieReviewer.Core.Interfaces;
using MovieReviewer.Service;
using MovieReviewer.Web.Blazor.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<IMovieClient, OmDbClient>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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
