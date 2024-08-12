namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using System.Text.RegularExpressions;
using MyFirstBlog.Dtos;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(String slug);
    void AddPost(Post post);
}

public class PostService : IPostService
{
    private DataContext _context;

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
        return getPost(slug).AsDto();
    }

    public void AddPost(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();
    }

    private Post getPost(string slug)
    {
        return _context.Posts.Where(a=>a.Slug==slug.ToString()).SingleOrDefault();
    }
}
