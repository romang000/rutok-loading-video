
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Options;

namespace Rutok.DownloadVideo.Infrastructure.BackgroundServices;

public class CreateVideoConsumer : BackgroundService
{
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IChannel _channel;
    private readonly IServiceProvider _serviceProvider;
    
    public CreateVideoConsumer(IOptions<RabbitMqOptions> rabbitMqOptions, IServiceProvider serviceProvider)
    {
        _rabbitMqOptions = rabbitMqOptions.Value;
        _serviceProvider = serviceProvider;
        
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password,
            VirtualHost = _rabbitMqOptions.VirtualHost
        };
        
        var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            try
            {
                var createVideoDto = JsonSerializer.Deserialize<VideoToCreate>(message, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })!;
                using var scope = _serviceProvider.CreateScope();
                var videoService = scope.ServiceProvider.GetRequiredService<IVideoService>();

                await videoService.CreateVideo(createVideoDto);

                await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
            }
            catch (Exception)
            {
                await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
            }
        };
        
        await _channel.BasicConsumeAsync(_rabbitMqOptions.CreateVideoQueueName, autoAck: false, consumer, cancellationToken: stoppingToken);
    }
}