using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Domain.Entities;


namespace Rutok.DownloadVideo.Application.Services;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<long?> CreateTag(TagToCreate tag)
    {
        var tagEntity = new TagEntity
        {
            //Id = Guid.NewGuid(),
            RuTag = tag.RuTag
        };
        
        var tagId = await tagRepository.Add(tagEntity);

        return tagId ?? null;
    }
    
    public async Task<List<TagToGet>> GetAllTags()
    {
        var tagEntity = await tagRepository.GetAll();
        var tag = tagEntity.Select(t => new TagToGet(t.Id, t.RuTag)).ToList();
        
        return tag;
    }

    public async Task<TagToGet?> GetTagById(long id)
    {
        var entity = await tagRepository.Get(id);
        
        if (entity == null)
        {
            return null;
        }
        
        var tag = new TagToGet(entity.Id, entity.RuTag);
        
        return tag;
    }

    public Task<long?> DeleteTagById(long id)
    {
        return tagRepository.Delete(id);
    }
}