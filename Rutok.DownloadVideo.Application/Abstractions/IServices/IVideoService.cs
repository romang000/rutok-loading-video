using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Domain.Entities;

namespace Rutok.DownloadVideo.Application.Abstractions.IServices;

public interface IVideoService
{
    Task<long?> CreateVideo(VideoToCreate video, long userId);
    Task<VideoToGet?> GetVideoById(long id);
    Task<List<VideoToGet>?> GetAllVideos();
    Task<bool> DeleteVideo(long id);
    Task<List<TagToGet>?> GetTagsByVideo(long id);
    Task<bool> BanVideo(long videoId);
    Task<bool> UnbanVideo(long videoId);
    Task<List<long>> GetVideoByUserId(long userId);
    Task<long?> ChangeLikesByVideoId(long videoId);
}