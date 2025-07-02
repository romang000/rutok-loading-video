namespace Rutok.DownloadVideo.Domain.Entities;

public class SubscriptionEntity : BaseEntity<long>
{
    public long UserId { get; set; }
    public long OnWhoSubscribedId { get; set; }
}
