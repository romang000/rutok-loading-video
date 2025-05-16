using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Services;
public class VideoService(IVideoRepository videoRepository, ITagRepository tagRepository) : IVideoService
{
    public async Task<Guid?> CreateVideo(VideoToCreate video)
    {
        var existingTags = await tagRepository.GetExisting(
            video.Tags.Select(t=> t.RuTag).ToList(),
            video.Tags.Select(t => t.EngTag).ToList()
            );

        var tagEntities = video.Tags
            .Where(vt => !existingTags.Any(et => et.RuTag == vt.RuTag || et.EngTag == vt.EngTag))
            .Select(t => new TagEntity
            {
                Id = Guid.NewGuid(),
                RuTag = t.RuTag,
                EngTag = t.EngTag,
            }).ToList();
        
        var entity = new VideoEntity
        {
            Id = Guid.NewGuid(),
            Name = video.Name,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = video.IsDeleted,
            Views = video.Views,
            Likes = video.Likes,
            Description = video.Description,
            UserId = video.UserId,
            Duration = video.Duration,
            IsBanned = video.IsBanned,
            IdVideo = video.IdVideo,
            CommentsAmount = video.CommentsAmount,
            Tags = tagEntities.Concat(existingTags).ToList()
        };
        
        var videoId = await videoRepository.Add(entity);
        
        return videoId;
    }

    public Task<List<VideoToGetById>> GetVideoById(Guid id)
    {
        throw new NotImplementedException();
    }
}