namespace Rutok.DownloadVideo.Domain.Entities;

public class UserCommentedVideoEntity
{
    public Guid UserId { get; set; }
    
    
    public Guid VideoId { get; set; }
}