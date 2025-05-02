namespace Rutok.DownloadVideo.Domain.Entities;

public class UserLikesEntity
{
    public Guid UserId { get; set; }
    
    public Guid VideoId { get; set; }
}