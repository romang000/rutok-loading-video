using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Infrastructure.Context;

public class DownloadVideoDbContext : DbContext
{
    public DownloadVideoDbContext(DbContextOptions<DownloadVideoDbContext> options) : base(options)
    { }
    
    public DbSet<VideoEntity> Videos { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<SubscriptionEntity> Subscriptions { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<UserCommentedVideoEntity> UserCommented { get; set; }
    public DbSet<UserLikesEntity> UserLikes { get; set; }
    public DbSet<UserWatchedVideoEntity> WatchedVideo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("download_video_service");
    }
}