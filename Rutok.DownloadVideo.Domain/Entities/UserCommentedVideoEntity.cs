namespace Rutok.DownloadVideo.Domain.Entities;

public class UserCommentedVideoEntity : BaseEntity<long>
{
    public long UserId { get; set; }
    public long VideoId { get; set; }
}