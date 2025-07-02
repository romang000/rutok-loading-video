namespace Rutok.DownloadVideo.Domain.Entities;

public class UserWatchedVideoEntity: BaseEntity<long>
{
    public long UserId { get; set; }
    public long VideoId { get; set; }
}