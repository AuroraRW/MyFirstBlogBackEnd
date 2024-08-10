using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Models;

namespace MyFirstBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            if (string.IsNullOrWhiteSpace(postDto.Title))
            {
                return BadRequest(new { errors = new[] { "Title cannot be blank" } });
            }

            var post = new { title = postDto.Title, description = postDto.Description };
            return CreatedAtAction(nameof(CreatePost), new { post });
        }
    }
}
