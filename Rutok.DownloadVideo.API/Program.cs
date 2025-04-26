using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DownloadVideoDbContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(DownloadVideoDbContext)));
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();