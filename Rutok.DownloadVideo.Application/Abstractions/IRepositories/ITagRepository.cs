using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface ITagRepository
{
    Task<List<TagEntity>> GetAll();
    Task<TagEntity?> Get(long id);
    Task<long?> Add(TagEntity entity);
    Task<long?> Delete(long id);
    Task<List<TagEntity>> GetExisting(List<string> ruTags, List<string> engTags);
}