namespace Rutok.DownloadVideo.Domain.Entities;

public class VideoEntity: BaseEntity<long>
{
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int Views { get; set; }
    public int Likes { get; set; }
    public string Description { get; set; } = string.Empty;
    public long UserId { get; set; }
    public TimeSpan Duration { get; set; }
    public bool IsBanned { get; set; } = false;
    public long IdVideo { get; set; }
    public int CommentsAmount { get; set; }
    
    public List<CommentEntity> Comments { get; set; } = [];
    public List<TagEntity> Tags { get; set; } = [];
    
}