namespace Rutok.DownloadVideo.DataAccess.Models;

public class UserWatchedVideoEntity
{
    public Guid UserId { get; set; }
    
    public Guid VideoId { get; set; }
}