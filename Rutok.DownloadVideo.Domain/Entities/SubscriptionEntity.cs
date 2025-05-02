using System.ComponentModel.DataAnnotations;

namespace Rutok.DownloadVideo.Domain.Entities;

public class SubscriptionEntity
{
    
    public Guid UserId { get; set; }
    
    [Key]
    public Guid OnWhoSubscribedId { get; set; }
}
