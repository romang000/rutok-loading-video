using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface IVideoRepository
{
    Task<Guid> Add(VideoEntity videoEntity);
    Task<VideoEntity?> Get(Guid id);
    Task<List<VideoEntity>?> GetAll();
    Task<bool> Delete(Guid id);
    Task<List<TagEntity>?> GetTags(Guid id);
    Task<bool> Ban(Guid videoId);
    Task<bool> Unban(Guid videoId);
    Task<bool> AddComment(Guid commentId, Guid videoId);
}