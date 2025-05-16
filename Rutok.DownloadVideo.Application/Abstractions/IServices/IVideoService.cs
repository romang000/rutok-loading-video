using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface IVideoService
{
    Task<Guid?> CreateVideo(VideoToCreate video);
    //Task<VideoEntity> AddTagToVideo(List<TagEntity> tagId, Guid videoId);
    Task<List<VideoToGetById>> GetVideoById(Guid id);
}