using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Rutok.DownloadVideo.Infrastructure.BackgroundServices;

public class MessageProducer : IMessageProducer
{
    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password"
        };
        
        var conn = factory.CreateConnection();
        using var channel = conn.CreateModel();
        
        channel.QueueDeclare(
            "videos",
            durable: true,
            exclusive: false
        );
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        channel.BasicPublish("", "videos", body:body);
    }
}