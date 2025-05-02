namespace Rutok.DownloadVideo.Domain.Entities;

public class TagEntity
{
    public Guid Id { get; set; }
    
    public string EngTag { get; set; } = string.Empty;
    
    public string RuTag { get; set; } = string.Empty;

    public List<VideoEntity> Videos { get; set; } = [];
}