using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface IVideoRepository
{
    Task<Guid> Add(VideoEntity videoEntity);
    Task<VideoEntity?> Get(Guid id);
    Task<List<VideoEntity>?> GetAll();
    Task<bool> Delete(Guid id);
}