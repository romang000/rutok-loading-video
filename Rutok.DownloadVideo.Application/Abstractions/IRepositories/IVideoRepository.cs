using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface IVideoRepository
{
    Task<long> Add(VideoEntity videoEntity);
    Task<VideoEntity?> Get(long id);
    Task<List<VideoEntity>?> GetAll();
    Task<bool> Delete(long id);
    Task<List<TagEntity>?> GetTags(long id);
    Task<bool> Ban(long videoId);
    Task<bool> Unban(long videoId);
    Task<bool> AddComment(long commentId, long videoId);
    Task<List<VideoEntity>> GetByUserId(long userId);
}