using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Tags;


namespace Rutok.DownloadVideo.Controllers;

[ApiController]
[Route("api/tags/")]
public class TagController(ITagService tagService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TagToGet>>> GetTags()
    {
        var tags = await tagService.GetAllTags();
        return Ok(tags);
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateTag([FromBody] TagToCreate tag)
    {
        var tagId = await tagService.CreateTag(tag);
        if (tagId is null)
        {
            return Conflict("Тэг с таким именем существует");
        }
        
        return Ok(tagId);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<List<TagToGetById>>> GetTags(Guid id)
    {
        var tag = await tagService.GetTagById(id);
        
        if (tag == null) return NotFound($"Tag with id {id} not found");
        
        return Ok(tag);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteTag(Guid id)
    {
        var tag = await tagService.DeleteTagById(id);
        
        if (tag == null) return NotFound($"Tag with id {id} not found");
        
        return Ok(tag);
    }
}