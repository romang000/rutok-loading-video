using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Infrastructure.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasOne(c => c.Video)
            .WithMany(v => v.Comments)
            .HasForeignKey(c => c.VideoId);
    }
}