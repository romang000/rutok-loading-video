using Rutok.DownloadVideo.Application.Models.Tags;

namespace Rutok.DownloadVideo.Application.Models.Video;

public record VideoToGet(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    bool IsDeleted,
    int View,
    int Likes,
    string Description,
    Guid UserId,
    TimeSpan Duration,
    bool IsBanned,
    long IdVideo,
    int CommentsCount,
    List<TagToGet> Tags
    );

