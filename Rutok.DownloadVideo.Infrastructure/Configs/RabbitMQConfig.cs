namespace Rutok.DownloadVideo.Infrastructure.Configs;

public class RabbitMqConfig
{
    public string HostName { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VirtualHost { get; set; } = null!;
    public string VideoDataQueueName { get; set; } = null!;
}