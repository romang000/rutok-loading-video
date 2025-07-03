using MapsterMapper;
using Rutok.DownloadVideo.Application.Abstractions.IRepositories;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Services;
public class VideoService(IVideoRepository videoRepository,
    ITagRepository tagRepository,
    IMapper mapper) : IVideoService
{
    public async Task<long?> CreateVideo(VideoToCreate video, long userId)
    {
        var existingTags = await tagRepository.GetExisting(
            video.Tags.Select(t => t.RuTag).ToList());
            //video.Tags.Select(t => t.EngTag).ToList()
            //);

        var tagEntities = video.Tags
            .Where(vt => existingTags.All(et => et.RuTag != vt.RuTag))
            .Select(t => new TagEntity
            {
                //Id = Guid.NewGuid(),
                RuTag = t.RuTag,
            }).ToList();
        
        var entity = new VideoEntity
        {
           // Id = Guid.NewGuid(),
            Name = video.Name,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            Views = 0,
            Likes = 0,
            Description = video.Description,
            UserId = userId,
           // Duration = video.Duration,
           // IsBanned = false,
            IdVideo = video.IdVideo,
            //CommentsAmount = video.CommentsAmount,
            Tags = tagEntities.Concat(existingTags).ToList()
        };
        
        var videoId = await videoRepository.Add(entity);
        
        return videoId;
    }

    public async Task<VideoToGet?> GetVideoById(long id)
    {
        var videoEntity = await videoRepository.Get(id);
        
        if (videoEntity == null) return null;

        var tags =  videoEntity.Tags.Select(t => new TagToGet(
            t.Id,
            t.RuTag
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

    public async Task<bool> DeleteVideo(long id)
    {
        var isDeleted = await videoRepository.Delete(id);
        return isDeleted;
    }

    public async Task<List<TagToGet>?> GetTagsByVideo(long id)
    {
        var tagsEntity = await videoRepository.GetTags(id);
        if (tagsEntity is null) return null;
        
        var tags = tagsEntity.Select(t => mapper.Map<TagEntity, TagToGet>(t)).ToList();
        
        return tags;
    }

    public async Task<bool> BanVideo(long videoId)
    {
        var result= await videoRepository.Ban(videoId);
        return result;
    }

    public async Task<bool> UnbanVideo(long videoId)
    {
        var result= await videoRepository.Unban(videoId);
        return result;
    }

    public async Task<List<long>> GetVideoByUserId(long userId)
    {
        var videosEntity = await videoRepository.GetByUserId(userId);
        var result = new List<long>();
        
        foreach (var v in videosEntity)
        {
            result.Add(v.Id);
        }
        return result;
    }

    public async Task<long?> ChangeLikesByVideoId(long videoId)
    {
        var id = await videoRepository.AddLike(videoId);
        return id;
    }
}