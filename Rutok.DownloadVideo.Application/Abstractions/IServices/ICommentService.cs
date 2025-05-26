using Rutok.DownloadVideo.Application.Models.Comments;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface ICommentService
{
    Task<Guid?> AddComment(CommentToAdd comment);
    Task<CommentsToGet?> GetById(Guid id);
    Task<List<CommentsToGet>?> GetByVideoId(Guid videoId);
    Task<List<CommentsToGet>?> GetByUserId(Guid userId);
    Task<bool> Update(Guid id, string text);
    Task<List<CommentsToGet>?> GetAll();
    Task<bool> Delete(Guid id);
}