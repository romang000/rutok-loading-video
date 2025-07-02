using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;
using Rutok.DownloadVideo.Application.Models.Video;


namespace Rutok.DownloadVideo.Controllers;

[ApiController]
[Route("api/videos/")]
public class VideoController(IVideoService service)
    //, IMessageProducer messageProducer)
    : ControllerBase
{  
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<long>> CreateVideo([FromBody] VideoToCreate video)
    {
        var userIdClaim = HttpContext.User.FindFirst("user_id");

        var videoId = await service.CreateVideo(video, long.Parse(userIdClaim.Value));
        
        //messageProducer.SendingMessage(video);

        return Ok(videoId);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoToGet>> GetVideoById([FromRoute] long id)
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
    [Authorize]
    [HttpGet("{id}/tags")]
    public async Task<ActionResult<List<TagToGet>>> GetAllTags([FromRoute] long id)
    {
        var response = await service.GetTagsByVideo(id);
        if (response == null) return NotFound();

        return Ok(response);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteVideo([FromRoute] long id)
    {
        var isDeleted = await service.DeleteVideo(id);
        if (isDeleted) return Ok();

        return NotFound();
    }
    [Authorize]
    [HttpPatch("{id}/ban")]
    public async Task<ActionResult<bool>> BanVideo([FromRoute] long id)
    {
        var isBan = await service.BanVideo(id);
        
        return isBan ? Ok() : NotFound();
    }
    [Authorize]
    [HttpPatch("{id}/unban")]
    public async Task<ActionResult<bool>> UnbanVideo([FromRoute] long id)
    {
        var isUnban = await service.UnbanVideo(id);
        return isUnban ? Ok() : NotFound();
    }
    
    [HttpGet("user/{id}")]
    public async Task<ActionResult<List<long>>> GetUserVideos([FromRoute] long id)
    {
        var response = await service.GetVideoByUserId(id);
        return Ok(response);
    }

}