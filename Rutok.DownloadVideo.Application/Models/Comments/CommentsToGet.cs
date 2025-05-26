using System.ComponentModel.DataAnnotations;

namespace Rutok.DownloadVideo.Application.Models.Comments;

public record CommentsToGet(
    [Required] Guid Id,
    [Required] Guid UserId,
    [Required] Guid VideoId,
    [Required][MaxLength(255)] string Text
    );