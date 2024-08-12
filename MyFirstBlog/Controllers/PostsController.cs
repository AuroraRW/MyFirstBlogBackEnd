using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Dtos;
using MyFirstBlog.Entities;
using MyFirstBlog.Services;
using System.Text.RegularExpressions;

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
            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST /posts
        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                    errors = new[] { "Title cannot be blank" }
                });
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Slug = Regex.Replace(request.Title.ToLower(), @"\s+", "-"),
                Body = request.Description,
                CreatedDate = DateTime.UtcNow
            };

            _postService.AddPost(post);

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                CreatedDate = post.CreatedDate
            };

            return CreatedAtAction(nameof(GetPost), new
            {
                slug = post.Slug
            }, new
            {
                post = postDto
            });
        }

    }
}
