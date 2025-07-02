using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Application.Extensions;
using Rutok.DownloadVideo.Extensions;
using Rutok.DownloadVideo.Infrastructure.Configs;
using Rutok.DownloadVideo.Infrastructure.Context;

//TODO:добавить ограничение на количество тего на видосе
var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.RegisterMapster();


builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMQ"));

string? connectionString = builder.Configuration.GetConnectionString("DownloadVideoDbContext");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DownloadVideoDbContext' is not configured.");
}


builder
    .AddData()
    .AddSwagger()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddBackgroundService()
    .AddOptions()
    .AddAuthenticationServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DownloadVideoDbContext>();
    db.Database.Migrate();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();