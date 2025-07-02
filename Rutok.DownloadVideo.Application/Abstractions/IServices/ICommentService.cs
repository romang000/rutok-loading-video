using Rutok.DownloadVideo.Application.Models.Comments;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface ICommentService
{
    Task<long?> AddComment(CommentToAdd comment, long userId);
    Task<CommentsToGet?> GetById(long id);
    Task<List<CommentsToGet>?> GetByVideoId(long videoId);
    Task<List<CommentsToGet>?> GetByUserId(long userId);
    Task<bool> Update(long id, string text);
    Task<List<CommentsToGet>?> GetAll();
    Task<bool> Delete(long id);
}