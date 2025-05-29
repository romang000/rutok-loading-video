using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;
using Rutok.DownloadVideo.Infrastructure.BackgroundServices;

namespace Rutok.DownloadVideo.Controllers;

[ApiController]
[Route("api/videos/")]
public class VideoController(IVideoService service, IMessageProducer messageProducer) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateVideo([FromBody] VideoToCreate video)
    {
        var videoId = await service.CreateVideo(video);
        
        messageProducer.SendingMessage(video);

        return Ok(videoId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VideoToGet>> GetVideoById([FromRoute] Guid id)
    {
        var video = await service.GetVideoById(id);

        if (video == null) return NotFound("Видео с таким id не найдено");

        return Ok(video);
    }

    [HttpGet]
    public async Task<ActionResult<List<VideoToGet>>> GetAllVideos()
    {
        var videos = await service.GetAllVideos();
        return Ok(videos);
    }

    [HttpGet("{id}/tags")]
    public async Task<ActionResult<List<TagToGet>>> GetAllTags([FromRoute] Guid id)
    {
        var response = await service.GetTagsByVideo(id);
        if (response == null) return NotFound();

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteVideo([FromRoute] Guid id)
    {
        var isDeleted = await service.DeleteVideo(id);
        if (isDeleted) return Ok();

        return NotFound();
    }

    [HttpPatch("{id}/ban")]
    public async Task<ActionResult<bool>> BanVideo([FromRoute] Guid id)
    {
        var isBan = await service.BanVideo(id);
        
        return isBan ? Ok() : NotFound();
    }

    [HttpPatch("{id}/unban")]
    public async Task<ActionResult<bool>> UnbanVideo([FromRoute] Guid id)
    {
        var isUnban = await service.UnbanVideo(id);
        return isUnban ? Ok() : NotFound();
    }

}