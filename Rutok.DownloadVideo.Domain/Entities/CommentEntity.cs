namespace Rutok.DownloadVideo.Domain.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public Guid VideoId { get; set; }
    
    public Guid UserId { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public int Length { get; set; }
    public VideoEntity? Video { get; set; }
}