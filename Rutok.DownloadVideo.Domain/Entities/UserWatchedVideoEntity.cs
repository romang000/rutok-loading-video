namespace Rutok.DownloadVideo.Domain.Entities;

public class UserWatchedVideoEntity
{
    public Guid UserId { get; set; }
    
    public Guid VideoId { get; set; }
}