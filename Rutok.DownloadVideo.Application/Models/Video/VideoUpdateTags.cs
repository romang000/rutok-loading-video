using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Models.Video;

public record VideoUpdateTags
    (
        List<TagEntity> Tags
    );