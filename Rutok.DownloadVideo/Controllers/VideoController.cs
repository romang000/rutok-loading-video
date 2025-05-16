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
}