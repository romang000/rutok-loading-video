using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Domain.Entities;
using Rutok.DownloadVideo.Infrastructure.Context;

namespace Rutok.DownloadVideo.Infrastructure.Repositories;

public class VideoRepository(DownloadVideoDbContext context) : IVideoRepository
{
    public async Task<Guid> Add(VideoEntity videoEntity)
    {
        var newEntity = await context.Videos.AddAsync(videoEntity);
        await context.SaveChangesAsync();
        
        return newEntity.Entity.Id;
    }

    public async Task<VideoEntity?> Get(Guid id)
    {
        return await context.Videos.FindAsync(id);
    }
    
    public async Task<List<VideoEntity>> Get()
    {
        return await context.Videos
            .Include(v => v.Tags)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<Guid> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}