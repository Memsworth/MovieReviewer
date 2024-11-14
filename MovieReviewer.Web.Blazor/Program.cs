using Auth0.AspNetCore.Authentication;
using MovieReviewer.Core.Infrastructure;
using MovieReviewer.Core.Infrastructure.Repositories;
using MovieReviewer.Core.Interfaces;
using MovieReviewer.Service;
using MovieReviewer.Web.Blazor.Features;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<IMovieClient, OmDbClient>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));


builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddRazorPages();
builder.Services.AddCascadingAuthenticationState();
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

app.UseRouting();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
