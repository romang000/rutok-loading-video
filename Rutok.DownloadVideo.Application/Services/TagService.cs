using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Domain.Entities;


namespace Rutok.DownloadVideo.Application.Services;

public class TagService(ITagRepository tagRepository) : ITagService
{
    public async Task<Guid> CreateTag(TagToCreate tag)
    {
        var tagEntity = new TagEntity
        {
            Id = Guid.NewGuid(),
            RuTag = tag.RuTag,
            EngTag = tag.EngTag,
        };
        
        var tagId = await tagRepository.Add(tagEntity);
        
        return tagId;
    }
    
    public async Task<List<TagToGet>> GetAllTags()
    {
        var tagEntity = await tagRepository.GetAll();
        var tag = tagEntity.Select(t => new TagToGet(t.Id, t.RuTag, t.EngTag)).ToList();
        
        return tag;
    }

    public async Task<TagToGetById?> GetTagById(Guid id)
    {
        var entity = await tagRepository.Get(id);
        
        if (entity == null)
        {
            return null!;
        }
        
        var tag = new TagToGetById(entity.Id, entity.RuTag, entity.EngTag);
        
        return tag;
    }

    public Task<Guid?> DeleteTagById(Guid id)
    {
        return tagRepository.Delete(id);
    }
}