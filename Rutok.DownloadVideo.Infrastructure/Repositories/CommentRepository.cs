using Microsoft.EntityFrameworkCore;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Domain.Entities;
using Rutok.DownloadVideo.Infrastructure.Context;

namespace Rutok.DownloadVideo.Infrastructure.Repositories;

public class CommentRepository(DownloadVideoDbContext context) : ICommentRepository
{
    public async Task<Guid> Add(CommentEntity comment)
    {
        var newEntity = await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        
        return newEntity.Entity.Id;
    }

    public async Task<CommentEntity?> GetById(Guid id)
    {
        var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        if (comment is null) return null;
        
        return comment;
    }

    public async Task<List<CommentEntity>?> GetByVideoId(Guid videoId)
    {
        var comments = await context.Comments.Where(c => c.VideoId == videoId).ToListAsync();
        
        if (!comments.Any()) return null;
        
        return comments;
    }

    public async Task<List<CommentEntity>?> GetByUserId(Guid userId)
    {
        var comments = await context.Comments.Where(c => c.UserId == userId).ToListAsync();
        
        if (!comments.Any()) return null;
        
        return comments;
    }
    
    public async Task<List<CommentEntity>?> GetAll()
    {
        var comments = await context.Comments.ToListAsync();
        if (!comments.Any()) return null;
        
        return comments;
    }

    public async Task<bool> Update(CommentEntity comment)
    { 
        var result = await context.Comments
            .Where(c => c.Id == comment.Id)
            .ExecuteUpdateAsync(s => s.SetProperty(c => c.Text, comment.Text)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
            );
        
        return result > 0;
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
        
        return result > 0;
    }
}