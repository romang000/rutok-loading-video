namespace Rutok.DownloadVideo.Domain.Entities;

public class VideoEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public int Views { get; set; }
    
    public int Likes { get; set; }

    public string Description { get; set; } = string.Empty;
    
    public Guid UserId { get; set; }
    
    public TimeSpan Duration { get; set; }
    
    public bool IsBanned { get; set; }
    
    public long IdVideo { get; set; }
    
    public int CommentsAmount { get; set; }
    
    
    //public Guid CommentId { get; set; }
    public List<CommentEntity> Comments { get; set; } = [];
    
    public List<TagEntity> Tags { get; set; } = [];
    
}