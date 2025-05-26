using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Infrastructure.Context;

namespace Rutok.DownloadVideo.Infrastructure.Repositories;

public class BaseRepository(DownloadVideoDbContext context) : IBaseRepository
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}