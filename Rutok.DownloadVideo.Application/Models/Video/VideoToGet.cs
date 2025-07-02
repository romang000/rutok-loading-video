using Rutok.DownloadVideo.Application.Models.Tags;

namespace Rutok.DownloadVideo.Application.Models.Video;

public record VideoToGet(
    long Id,
    string Name,
    DateTime CreatedAt,
    bool IsDeleted,
    int View,
    int Likes,
    string Description,
    long UserId,
    TimeSpan Duration,
    bool IsBanned,
    long IdVideo,
    int CommentsCount,
    List<TagToGet> Tags
    );

