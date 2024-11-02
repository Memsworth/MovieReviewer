using MovieReviewer.Api.Utilities;
using MovieReviewer.Core.Infrastructure;
using MovieReviewer.Core.Infrastructure.Repositories;
using MovieReviewer.Core.Interfaces;
using MovieReviewer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<ReviewService>();
//builder.Services.AddScoped<AuthRepository>();
//builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IMovieClient, OmDbClient>();

builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddControllers();

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
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
