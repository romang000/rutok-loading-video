namespace Rutok.DownloadVideo.Domain.Entities;

public class UserWatchedVideoEntity: BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid VideoId { get; set; }
}