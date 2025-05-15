using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Domain.Entities;
using Rutok.DownloadVideo.Infrastructure.Context;

namespace Rutok.DownloadVideo.Infrastructure.Repositories;

public class TagRepository(DownloadVideoDbContext context) : ITagRepository
{
    public async Task<Guid?> Add(TagEntity entity)
    {
        bool isHaveThisTag = await context.Tags.AnyAsync(t => t.RuTag == entity.RuTag && t.EngTag == entity.EngTag);
        if (isHaveThisTag)
        {
            return null;
        }
        
        var newEntity = await context.Tags.AddAsync(entity);
        await context.SaveChangesAsync();
        return newEntity.Entity.Id;
    }
    
    public async Task<List<TagEntity>> GetAll()
    {
        return await context.Tags
            .Include(t => t.Videos)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TagEntity?> Get(Guid id)
    {
        return await context.Tags.FindAsync(id);
    }
    
    public async Task<Guid?> Delete(Guid id)
    { 
        var rows = await context.Tags
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
        
        return rows > 0 ? id : null;
    }
    
    public async Task<List<TagEntity>> GetExisting(List<string> ruTags, List<string> engTags)
    {
        return await context.Tags
            .Where(t => ruTags.Contains(t.RuTag) || engTags.Contains(t.EngTag))
            .ToListAsync();
    }
}