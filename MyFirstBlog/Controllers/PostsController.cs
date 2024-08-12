namespace MyFirstBlog.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;

[ApiController]
[Route("posts")]

public class PostsController : ControllerBase
{
    private IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public IEnumerable<PostDto> GetPosts()
    {
        return _postService.GetPosts();
    }

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

    [HttpPost]
    public ActionResult<PostDto> CreatePost(CreatePostDto postDto)
    {
        try
        {
            var createdPost = _postService.CreatePost(postDto);
            return CreatedAtAction(nameof(GetPost), new { slug = createdPost.Slug }, createdPost);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { errors = new[] { ex.Message } });
        }
    }
}