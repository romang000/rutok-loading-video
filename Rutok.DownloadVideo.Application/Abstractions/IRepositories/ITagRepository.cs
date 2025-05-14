using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IRepositories;

public interface ITagRepository
{
    Task<List<TagEntity>> GetAll();
    Task<TagEntity?> Get(Guid id);
    Task<Guid?> Add(TagEntity entity);
    Task<Guid?> Delete(Guid id);
    Task<bool> FindByName(string name);
    Task<List<TagEntity>> GetExisting(List<string> ruTags, List<string> engTags);
}