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
builder.Services.AddMyServices();
// Add services to the container.


builder.Services.AddAuthServices(builder.Configuration);

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
