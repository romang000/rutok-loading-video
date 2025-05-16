using MapsterMapper;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Services;
public class VideoService(IVideoRepository videoRepository, ITagRepository tagRepository, IMapper mapper) : IVideoService
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

    public async Task<VideoToGet?> GetVideoById(Guid id)
    {
        var videoEntity = await videoRepository.Get(id);
        
        if (videoEntity == null) return null;

        var tags =  videoEntity.Tags.Select(t => new TagToGet(
            t.Id,
            t.RuTag,
            t.EngTag
        ))
            .ToList();
        
        var video = new VideoToGet(
            videoEntity.Id,
            videoEntity.Name,
            videoEntity.CreatedAt,
            videoEntity.IsDeleted,
            videoEntity.Views,
            videoEntity.Likes,
            videoEntity.Description,
            videoEntity.UserId,
            videoEntity.Duration,
            videoEntity.IsBanned,
            videoEntity.IdVideo,
            videoEntity.CommentsAmount,
            tags
        );
        
        return video;
    }

    public async Task<List<VideoToGet>?> GetAllVideos()
    {
        var videosEntity = await videoRepository.GetAll();
        
        if (videosEntity is null) return null;
        
        var videos = videosEntity.Select(v => new VideoToGet(
            v.Id,
            v.Name,
            v.CreatedAt,
            v.IsDeleted,
            v.Views,
            v.Likes,
            v.Description,
            v.UserId,
            v.Duration,
            v.IsBanned,
            v.IdVideo,
            v.CommentsAmount,
            v.Tags.Select(mapper.Map<TagEntity, TagToGet>).ToList()
        )).ToList();
        return videos;
    }

    public async Task<bool> DeleteVideo(Guid id)
    {
        var isDeleted = await videoRepository.Delete(id);
        return isDeleted;
    }
}