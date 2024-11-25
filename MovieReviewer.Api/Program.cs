using System.Text.Json;
using FluentValidation;
using MovieReviewer.Api.Services;
using MovieReviewer.Api.Utilities;
using MovieReviewer.Data;
using MovieReviewer.Data.Repositories;
using MovieReviewer.Service;
using MovieReviewer.Service.Validation;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IMovieRepository,MovieRepository>();
builder.Services.AddScoped<IMovieClient, OmDbClient>();

builder.Services.AddScoped<MovieService>();
builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

builder.Services.AddScoped<IValidator<CreateMovieDto>, CreateMovieDtoValidation>();
builder.Services.AddScoped<IValidator<UpdateMovieDto>, UpdateMovieDtoValidation>();
builder.Services.AddControllers().AddJsonOptions(options => 
    options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddCors(options =>
{
    options.AddPolicy("apiKey", builder => 
        builder.WithOrigins("https://localhost:7011")
        .AllowAnyMethod()
        .AllowAnyHeader());
});

/*builder.Services.AddIdentity<ApiUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();*/


/*builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:SecurityKey").Value!);
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true
        };
    });
*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("apiKey");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();