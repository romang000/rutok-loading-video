using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface ICommentRepository
{
    Task<Guid> Add(CommentEntity comment);
    Task<CommentEntity?> GetById(Guid id);
    Task<List<CommentEntity>?> GetByVideoId(Guid videoId);
    Task<List<CommentEntity>?> GetByUserId(Guid userId);
    Task<bool> Update(CommentEntity comment);
    Task<List<CommentEntity>?> GetAll();
    Task<bool> Delete(Guid id);
}