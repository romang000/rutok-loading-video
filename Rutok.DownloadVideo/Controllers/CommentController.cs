using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rutok.DownloadVideo.Application.Abstractions.IServices;
using Rutok.DownloadVideo.Application.Models.Comments;

namespace Rutok.DownloadVideo.Controllers;

//[Authorize]
[ApiController]
[Route("api/comments")]
public class CommentController(ICommentService commentService) : ControllerBase
{
    //TODO:доставать из кукисов userId
    //TODO:по этому ендпоинту просто другой мс обращается
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<long?>> AddCommentToVideo(CommentToAdd comment)
    {
        var userIdClaim = HttpContext.User.FindFirst("user_id");
        
        var response = await commentService.AddComment(comment, long.Parse(userIdClaim.Value));
        if (response is null) return NotFound("Video not found");
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentsToGet>>> GetAllComments()
    {
        var response = await commentService.GetAll();
        if (response is null) return NotFound("Comments not found");
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentsToGet>> GetComment([FromRoute] long id)
    {
        var response = await commentService.GetById(id);
        
        if (response is null) return NotFound("Comment not found");
        
        return Ok(response);
    }

    [HttpGet("videos/{id}")]
    public async Task<ActionResult<CommentsToGet>> GetCommentsByVideo([FromRoute] long id)
    {
        var response = await commentService.GetByVideoId(id);
        if (response is null) return NotFound("Comments not found");
        
        return Ok(response);
    }

    [HttpGet("users/{id}")]
    public async Task<ActionResult<CommentsToGet>> GetCommentsByUser([FromRoute] long id)
    {
        var response = await commentService.GetByUserId(id);
        if (response is null) return NotFound("Comments not found");
        
        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateComment([FromRoute] long id, [FromHeader] string text)
    {
        var response = await commentService.Update(id, text);
        if (!response) return NotFound("Comment not found");
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment([FromRoute] long id)
    {
        var response = await commentService.Delete(id);
        if (!response) return NotFound("Comment not found");
        
        return Ok(response);
    }
}