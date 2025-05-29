namespace Rutok.DownloadVideo.Infrastructure.BackgroundServices;

public interface IMessageProducer
{
    void SendingMessage<T>(T message);
}