namespace MyFirstBlog.Services;

using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using System.Text.RegularExpressions;
using MyFirstBlog.Dtos;

public interface IPostService
{
    IEnumerable<PostDto> GetPosts();
    PostDto GetPost(String slug);
    PostDto CreatePost(CreatePostDto postDto);
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

    private Post getPost(string slug)
    {
        return _context.Posts.Where(a=>a.Slug==slug.ToString()).SingleOrDefault();
    }

    public PostDto CreatePost(CreatePostDto postDto)
	{
        var slug = GenerateSlug(postDto.Title);

		var post = new Post
		{
			Title = postDto.Title,
			Body = postDto.Body,
			Slug = slug,
			CreatedDate = DateTime.UtcNow
		};

		_context.Posts.Add(post);
		_context.SaveChanges();

		return post.AsDto();
	}

    private string GenerateSlug(string title)
	{
        string slug = title.ToLower();
		slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
		slug = Regex.Replace(slug, @"\s+", "-");
        slug = slug.Trim();

        string baseSlug = slug;
        int slugCount = 1;
        while (_context.Posts.Any(a => a.Slug == slug))
		{
			slug = $"{baseSlug}-{slugCount}";
			slugCount++;
		}

		return slug;
	}
}
