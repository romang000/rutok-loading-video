using MapsterMapper;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Comments;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Services;

public class CommentService (ICommentRepository commentRepository,
    IVideoRepository videoRepository,
    IBaseRepository baseRepository,
    IMapper mapper) : ICommentService
{
    public async Task<Guid?> AddComment(CommentToAdd comment)
    {
        var commentEntity = new CommentEntity
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Text = comment.Text,
            VideoId = comment.VideoId,
            UserId = comment.UserId,
            IsDeleted = false
        };
        
        var commentId = await commentRepository.Add(commentEntity);
        
        var resultADdCommentToVideo = await videoRepository.AddComment(commentId, comment.VideoId);

        await baseRepository.SaveChangesAsync();
        
        if (!resultADdCommentToVideo) return null;
        
        return commentId;
    }

    public async Task<CommentsToGet?> GetById(Guid id)
    {
        var commentEntity = await commentRepository.GetById(id);
        if (commentEntity == null) return null;
        
        var comment = mapper.Map<CommentEntity, CommentsToGet>(commentEntity);
        
        return comment;
    }

    public async Task<List<CommentsToGet>?> GetByVideoId(Guid videoId)
    {
        var commentsListEntity = await commentRepository.GetByVideoId(videoId);
        if (commentsListEntity == null) return null;
        
        var comments = mapper.Map<List<CommentEntity>, List<CommentsToGet>>(commentsListEntity);
        
        return comments;
    }


    public async Task<List<CommentsToGet>?> GetByUserId(Guid userId)
    {
        var commentsListEntity = await commentRepository.GetByUserId(userId);
        if (commentsListEntity == null) return null;
        
        var comments = mapper.Map<List<CommentEntity>, List<CommentsToGet>>(commentsListEntity);
        
        return comments;
    }

    public async Task<bool> Update(Guid id, string text)
    {
        var commentToUpdate = new CommentsToUpdate(text, id);
        
        var commentEntity = mapper.Map<CommentsToUpdate, CommentEntity>(commentToUpdate);
        
        var isUpdate = await commentRepository.Update(commentEntity);
        if (!isUpdate) return false;
        
        return true;
    }

    public async Task<List<CommentsToGet>?> GetAll()
    {
        var commentsListEntity = await commentRepository.GetAll();
        if (commentsListEntity == null) return null;
        
        var comments = mapper.Map<List<CommentEntity>, List<CommentsToGet>>(commentsListEntity);
        
        return comments;
    }

    public async Task<bool> Delete(Guid id)
    {
        var isDelete = await commentRepository.Delete(id);
        if (!isDelete) return false;
        
        return true;
    }
}