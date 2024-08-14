using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Services;
using MyFirstBlog.Dtos;
using System.Collections.Generic;

namespace MyFirstBlog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;

        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts
        [HttpGet]
        public ActionResult<IEnumerable<PostDto>> GetPosts()
        {
            var posts = _postService.GetPosts();
            return Ok(posts);
        }

        // GET: api/posts/{slug}
        [HttpGet("{slug}")]
        public ActionResult<PostDto> GetPost(string slug)
        {
            var post = _postService.GetPost(slug);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        // POST: api/posts/new
        [HttpPost("new")]
        public ActionResult<PostDto> AddPost([FromBody] CreatePostDto createPostDto)
        {
            if (string.IsNullOrWhiteSpace(createPostDto.Title))
            {
                return BadRequest(new { errors = new[] { "Title cannot be blank" } });
            }

            var post = _postService.CreatePost(createPostDto);

            return CreatedAtAction(nameof(GetPost), new { slug = post.Slug }, post);
        }
    }
}