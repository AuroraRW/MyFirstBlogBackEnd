namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using MyFirstBlog.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(string slug);
    Task<PostDto> CreatePostAsync(Post post); // Add this method
}

public class PostService : IPostService
{
    private readonly DataContext _context;

    public PostService(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<PostDto> GetPosts()
    {
        return _context.Posts.Select(post => post.AsDto());
    }

    public PostDto GetPost(string slug)
    {
        return getPost(slug)?.AsDto();
    }

    public async Task<PostDto> CreatePostAsync(Post post)
    {
        // Add the post to the database context
        _context.Posts.Add(post);

        // Save changes asynchronously
        await _context.SaveChangesAsync();

        // Return the created post as a DTO
        return post.AsDto();
    }

    private Post getPost(string slug)
    {
        return _context.Posts.SingleOrDefault(a => a.Slug == slug);
    }
}
