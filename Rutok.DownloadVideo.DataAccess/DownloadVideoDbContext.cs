using Microsoft.EntityFrameworkCore;

namespace Rutok.DownloadVideo.DataAccess;

public class DownloadVideoDbContext : DbContext
{
    public DownloadVideoDbContext(DbContextOptions<DownloadVideoDbContext> options) : base(options)
    { }
    
    
}