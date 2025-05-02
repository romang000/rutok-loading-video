using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;

namespace Rutok.DownloadVideo.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<ActionResult<List<TagToGet>>> GetTags()
    {
        var tags = await tagService.GetAllTags();
        return Ok(tags);
    }

    [HttpPost("create")]
    public async Task<ActionResult<Guid>> CreateTag([FromBody] TagToCreate tag)
    {
        var tagId = await tagService.CreateTag(tag);
        return Ok(tagId);
    }

    [HttpPost("get/{tagId}")]
    public async Task<ActionResult<List<TagToGetById>>> GetTags(Guid tagId)
    {
        var tag = await tagService.GetTagById(tagId);
        
        if (tag == null) return NotFound($"Tag with id {tagId} not found");
        
        return Ok(tag);
    }

    [HttpDelete("delete/{tagId}")]
    public async Task<ActionResult<Guid>> DeleteTag(Guid tagId)
    {
        var tag = await tagService.DeleteTagById(tagId);
        
        if (tag == null) return NotFound($"Tag with id {tagId} not found");
        
        return Ok(tag);
    }
}