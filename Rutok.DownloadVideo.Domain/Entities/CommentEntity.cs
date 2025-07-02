namespace Rutok.DownloadVideo.Domain.Entities;

public class CommentEntity : BaseEntity<long>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Text { get; set; } = string.Empty;
    public long VideoId { get; set; }
    public long UserId { get; set; }
    public bool IsDeleted { get; set; }
    //public int Length { get; set; }
    public VideoEntity? Video { get; set; }
}