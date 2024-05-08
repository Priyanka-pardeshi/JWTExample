

using JWTProject;
using JWTProject.Store;
using JWTProject.Store.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var configbuilder = new ConfigurationBuilder()
           .AddJsonFile("appSettings.json");


builder.Services.Configure<appsettings>(configbuilder.Build());
builder.Services.AddSingleton<appsettings>();
builder.Services.AddScoped<ILoginStore, LoginStore>();
builder.Services.AddScoped<IAccessInfoStore, AccessInfoStore>();
// Add services to the container.


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration.GetSection("Jwt")["Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt")["Issuer"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["Secret"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(Convert.ToInt32(builder.Configuration.GetSection("Jwt")["ClockSkew"]))
    };
    option.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Call this to skip the default logic and avoid using the default response
            context.HandleResponse();

            // Write to the response in any way you wish
            context.Response.StatusCode = 401;
            context.Response.Headers.Append("my-custom-header", "custom-value");
            await context.Response.WriteAsync("You are not authorized! (or some other custom message)");
        }
    };
});


builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
