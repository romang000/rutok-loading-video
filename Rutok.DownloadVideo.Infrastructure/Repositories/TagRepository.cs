using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Domain.Entities;
using Rutok.DownloadVideo.Infrastructure.Context;

namespace Rutok.DownloadVideo.Infrastructure.Repositories;

public class TagRepository(DownloadVideoDbContext context) : ITagRepository
{
    public async Task<Guid> Add(TagEntity entity)
    {
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
}