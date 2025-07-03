using System.ComponentModel.DataAnnotations;

namespace Rutok.DownloadVideo.Application.Models.Comments;

public record CommentsToGet(
    [Required] long Id,
    [Required] long UserId,
    [Required] long VideoId,
    [Required][MaxLength(255)] string Text
    );