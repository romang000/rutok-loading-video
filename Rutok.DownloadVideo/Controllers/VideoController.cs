using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Video;

namespace Rutok.DownloadVideo.Controllers;

[ApiController]
[Route("api/videos/")]
public class VideoController(IVideoService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVideo([FromBody] VideoToCreate video)
    {
        var videoId = await service.CreateVideo(video);
        
        return Ok(videoId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoToGet>> GetVideoById(Guid id)
    {
        var video = await service.GetVideoById(id);
        
        if(video == null) return NotFound("Видео с таким id не найдено");
        
        return Ok(video);
    }

    [HttpGet]
    public async Task<ActionResult<List<VideoToGet>>> GetAllVideos()
    {
        var videos = await service.GetAllVideos();
        return Ok(videos);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteVideo(Guid id)
    {
        var isDeleted = await service.DeleteVideo(id);
        if (isDeleted) return Ok();
        
        return NotFound();
    }
}