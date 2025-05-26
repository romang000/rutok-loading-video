namespace Rutok.DownloadVideo.Domain.Entities;

public class UserCommentedVideoEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid VideoId { get; set; }
}