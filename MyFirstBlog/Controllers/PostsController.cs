namespace MyFirstBlog.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyFirstBlog.Dtos;
    using MyFirstBlog.Services;
    using MyFirstBlog.Entities; 
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;

    [ApiController]
    [Route("posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET /posts
        [HttpGet]
        public IEnumerable<PostDto> GetPosts()
        {
            return _postService.GetPosts();
        }

        // GET /posts/{slug}
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

        // POST /posts
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDto newPost)
        {
            // Validate the incoming data
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generate a unique slug based on the title (assuming slug should be unique and URL-friendly)
            var slug = GenerateSlug(newPost.Title);

            // Create a new Post entity from the DTO
            var post = new Post
            {
                Id = Guid.NewGuid(), // Generate a new Guid for the post
                Title = newPost.Title,
                Slug = slug, // Generate a slug for the post
                Body = newPost.Body, // Assuming 'Description' in PostDto corresponds to 'Body' in Post entity
                CreatedDate = DateTime.UtcNow // Set the current date and time as the creation date
            };

            // Use the service to save the new post
            var createdPost = await _postService.CreatePostAsync(post);

            // Return the result with a 201 Created status code
            return CreatedAtAction(nameof(GetPost), new { slug = createdPost.Slug }, createdPost);
        }

        private string GenerateSlug(string title)
        {
            // Convert to lower case
            string slug = title.ToLower();

            // Replace spaces with hyphens
            slug = slug.Replace(" ", "-");

            // Remove invalid characters
            slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");

            // Trim hyphens from the ends
            slug = slug.Trim('-');

            return slug;
        }


    }
}
