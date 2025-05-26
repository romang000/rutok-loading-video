namespace Rutok.DownloadVideo.Application.Models.Comments;

public record CommentToAdd(
    string Text,
    Guid VideoId,
    Guid UserId
    );