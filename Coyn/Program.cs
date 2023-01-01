using System.Text;
using Coyn.Databases;
using Coyn.Exception;
using Coyn.Plaid;
using Coyn.Token.Service;
using Coyn.Transaction.Service;
using Coyn.User.Service;
using Coyn.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Databases
builder.Services.AddDbContext<ApplicationApiDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("CoynConnectionString");
    options.UseNpgsql(connectionString);
});
// builder.Services.AddDbContext<ApplicationApiDbContext>(options => options.UseInMemoryDatabase("Coyn"));

// Controllers
builder.Services.AddControllers();

// Swagger & Endpoints
builder.Services.AddEndpointsApiExplorer();

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var secretToken = builder.Configuration.GetSection("AppSettings:Token").Value;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretToken)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddSingleton<PlaidService>();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


app.Run();