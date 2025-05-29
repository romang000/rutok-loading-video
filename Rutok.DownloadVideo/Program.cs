using Rutok.DownloadVideo.Application.Extensions;
using Rutok.DownloadVideo.Extensions;
//TODO:добавить ограничение на количество тего на видосе
var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.RegisterMapster();

builder
    .AddData()
    .AddSwagger()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddBackgroundService()
    .AddOptions();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();