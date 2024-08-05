using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Dtos;
using MyFirstBlog.Entities;
using MyFirstBlog.Services;
using System.Collections.Generic;

namespace MyFirstBlog.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // Get /posts
        [HttpGet]
        public IEnumerable<PostDto> GetPosts()
        {
            return _postService.GetPosts();
        }

        // Get /posts/:slug
        [HttpGet("{slug}")]
        public ActionResult<PostDto> GetPost(string slug)
        {
            var post = _postService.GetPost(slug);

            if (post is null)
            {
                return NotFound();
            }
            return post;
        }

        // Post /posts
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostDto createPostDto)
        {
            if (string.IsNullOrWhiteSpace(createPostDto.Title))
            {
                return BadRequest(new { errors = new[] { "Title cannot be blank" } });
            }

            var post = _postService.CreatePost(createPostDto.Title, createPostDto.Description);

            return CreatedAtAction(nameof(CreatePost), new { post = post });
        }
    }
}
