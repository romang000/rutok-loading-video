namespace Rutok.DownloadVideo.Application.Models.Comments;

public record CommentToAdd(
    string Text,
    long VideoId
    //Guid UserId
    );