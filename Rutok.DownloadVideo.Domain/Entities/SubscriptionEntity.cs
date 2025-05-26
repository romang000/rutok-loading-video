using System.ComponentModel.DataAnnotations;

namespace Rutok.DownloadVideo.Domain.Entities;

public class SubscriptionEntity : BaseEntity<Guid>
{
    public Guid UserId { get; set; }
    public Guid OnWhoSubscribedId { get; set; }
}
