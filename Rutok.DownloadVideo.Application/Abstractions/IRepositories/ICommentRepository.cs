using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface ICommentRepository
{
    Task<long> Add(CommentEntity comment);
    Task<CommentEntity?> GetById(long id);
    Task<List<CommentEntity>?> GetByVideoId(long videoId);
    Task<List<CommentEntity>?> GetByUserId(long userId);
    Task<bool> Update(CommentEntity comment);
    Task<List<CommentEntity>?> GetAll();
    Task<bool> Delete(long id);
}