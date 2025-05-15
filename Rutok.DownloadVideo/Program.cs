using Rutok.DownloadVideo.Application.Extensions;
using Rutok.DownloadVideo.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.RegisterMapster();

builder
    .AddData()
    .AddSwagger()
    .AddApplicationServices()
    .AddIntegrationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();