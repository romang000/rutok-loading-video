using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface IVideoService
{
    Task<Guid?> CreateVideo(VideoToCreate video);
    Task<VideoToGet?> GetVideoById(Guid id);
    Task<List<VideoToGet>?> GetAllVideos();
    Task<bool> DeleteVideo(Guid id);
}