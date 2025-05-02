using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Infrastructure.Configuration;

public class TagConfiguration : IEntityTypeConfiguration<TagEntity>
{
    public void Configure(EntityTypeBuilder<TagEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder
            .HasMany(t => t.Videos)
            .WithMany(v => v.Tags);
    }
}