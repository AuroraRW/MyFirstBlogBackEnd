using System;
using System.Linq;
using MyFirstBlog.Helpers;
using MyFirstBlog.Entities;
using MyFirstBlog.Dtos;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyFirstBlog.Services
{
    public interface IPostService
    {
        IEnumerable<PostDto> GetPosts();
        PostDto GetPost(string slug);
        PostDto CreatePost(CreatePostDto postDto);
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

        public PostDto CreatePost(CreatePostDto postDto)
        {
            if (string.IsNullOrWhiteSpace(postDto.Title))
            {
                throw new ArgumentException("Title cannot be blank.");
            }

            var newPost = new Post
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Slug = GenerateSlug(postDto.Title),
                Body = postDto.Description,
                CreatedDate = DateTime.UtcNow
            };

            _context.Posts.Add(newPost);
            _context.SaveChanges();

            return newPost.AsDto();
        }

        private Post getPost(string slug)
        {
            return _context.Posts.SingleOrDefault(a => a.Slug == slug);
        }

        private string GenerateSlug(string title)
        {
            return Regex.Replace(title.ToLower(), @"\s+", "-");
        }
    }
}
