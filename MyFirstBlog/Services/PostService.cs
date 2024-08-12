using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using System.Text.RegularExpressions;
using MyFirstBlog.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace MyFirstBlog.Services
{
    public interface IPostService
    {
        IEnumerable<PostDto> GetPosts();
        PostDto GetPost(string slug);
        Post CreatePost(string title, string description, string body);
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
            return _context.Posts.Select(post => new PostDto
            {
                Title = post.Title,
                Description = post.Description,
                Slug = post.Slug,
                CreatedDate = post.CreatedDate 
            });
        }

        public PostDto GetPost(string slug)
        {
            var post = getPost(slug);
            if (post == null) return null;

            return new PostDto
            {
                Title = post.Title,
                Description = post.Description,
                Slug = post.Slug,
                CreatedDate = post.CreatedDate 
            };
        }

        private Post getPost(string slug)
        {
            return _context.Posts.FirstOrDefault(a => a.Slug == slug);
        }

        public Post CreatePost(string title, string description, string body)
        {
            var post = new Post
            {
                Title = title,
                Description = description,
                Body = body,
                Slug = GenerateSlug(title),
                CreatedDate = DateTime.UtcNow 
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return post;
        }

        private string GenerateSlug(string title)
        {
            var slug = Regex.Replace(title.ToLower(), @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            slug = Regex.Replace(slug, @"\s", "-");
            return slug;
        }
    }
}
