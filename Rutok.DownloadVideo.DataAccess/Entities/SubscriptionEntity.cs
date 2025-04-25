using System.ComponentModel.DataAnnotations;

namespace Rutok.DownloadVideo.DataAccess.Entities;

public class SubscriptionEntity
{
    
    public Guid UserId { get; set; }
    
    [Key]
    public Guid OnWhoSubscribedId { get; set; }
}
