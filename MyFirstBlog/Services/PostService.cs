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
        Post CreatePost(string title, string description); // --
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
            return getPost(slug).AsDto();
        }

        private Post getPost(string slug)
        {
            return _context.Posts.Where(a => a.Slug == slug.ToString()).SingleOrDefault();
        }

        public Post CreatePost(string title, string description)
        {
            var post = new Post
            {
                Title = title,
                Description = description,
                Slug = GenerateSlug(title)
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
