using Rutok.DownloadVideo.Application.Models.Tags;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface ITagService
{
    Task<Guid> CreateTag(TagToCreate tag);
    Task<List<TagToGet>> GetAllTags();
    Task<TagToGetById?> GetTagById(Guid id);
    Task<Guid?> DeleteTagById(Guid id);
}