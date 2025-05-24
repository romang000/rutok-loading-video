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
        return await context.Videos
            .Include(v => v.Tags)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
    
    public async Task<List<VideoEntity>?> GetAll()
    {
        return await context.Videos
            .Include(v => v.Tags)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        var rows = await context.Videos
            .Where(v => v.Id == id)
            .ExecuteDeleteAsync();
        
        return rows > 0;
    }

    public async Task<List<TagEntity>?> GetTags(Guid videoId)
    {
        var video = await context.Videos.Include(videoEntity => videoEntity.Tags).FirstOrDefaultAsync(v => v.Id == videoId);
        if (video is null) return null;
        
        return video.Tags;
    }

    public async Task<bool> Ban(Guid videoId)
    {
        var video = await context.Videos.FirstOrDefaultAsync(v => v.Id == videoId);
        if (video is null) return false;
        
        video.IsBanned = true;
        await context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> Unban(Guid videoId)
    {
        var video = await context.Videos.FirstOrDefaultAsync(v => v.Id == videoId);
        if (video is null) return false;
        
        video.IsBanned = false;
        await context.SaveChangesAsync();
        
        return true;
    }
}