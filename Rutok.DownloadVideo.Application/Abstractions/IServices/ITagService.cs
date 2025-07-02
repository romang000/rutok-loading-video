using Rutok.DownloadVideo.Application.Models.Tags;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface ITagService
{
    Task<long?> CreateTag(TagToCreate tag);
    Task<List<TagToGet>> GetAllTags();
    Task<TagToGet?> GetTagById(long id);
    Task<long?> DeleteTagById(long id);
    
}