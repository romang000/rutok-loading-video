using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Infrastructure.Configuration;

public class VideoConfiguration : IEntityTypeConfiguration<VideoEntity>
{
    public void Configure(EntityTypeBuilder<VideoEntity> builder)
    {
        builder.HasKey(v => v.Id);
        
        builder
            .HasMany(v => v.Comments)
            .WithOne(c => c.Video)
            .HasForeignKey(с => с.VideoId);

        builder
            .HasMany(t => t.Tags)
            .WithMany(v => v.Videos);

    }
}