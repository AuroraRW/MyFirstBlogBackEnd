using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;
using MyFirstBlog.Entities;
using MyFirstBlog.Dtos;
using Microsoft.Extensions.Configuration;
using MyFirstBlog.Helpers;

namespace MyFirstBlog.Services
{
    public class PostService
    {
        private readonly string _connectionString;

        public PostService(IConfiguration configuration)
        {
            _connectionString = ConnectionHelper.GetConnectionString(configuration);
        }

        public IEnumerable<PostDto> GetPosts()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var posts = connection.Query<Post>("SELECT * FROM public.\"Posts\" ORDER BY \"Id\" ASC").ToList();

            return posts.Select(post => new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                CreatedDate = post.CreatedDate
            }).ToList();
        }

        public PostDto GetPost(string slug)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var post = connection.QuerySingleOrDefault<Post>("SELECT * FROM public.\"Posts\" WHERE \"Slug\" = @Slug", new { Slug = slug });
            if (post == null)
                return null;

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Slug = post.Slug,
                Body = post.Body,
                CreatedDate = post.CreatedDate
            };
        }

        public PostDto CreatePost(CreatePostDto createPostDto)
        {
            var newPost = new Post
            {
                Id = Guid.NewGuid(),
                Title = createPostDto.Title,
                Slug = string.IsNullOrEmpty(createPostDto.Slug) ? GenerateSlug(createPostDto.Title) : createPostDto.Slug,
                Body = createPostDto.Body,
                CreatedDate = createPostDto.CreatedDate == default ? DateTime.UtcNow : createPostDto.CreatedDate
            };

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Execute("INSERT INTO public.\"Posts\" (\"Id\", \"Title\", \"Slug\", \"Body\", \"CreatedDate\") VALUES (@Id, @Title, @Slug, @Body, @CreatedDate)", newPost);

            return new PostDto
            {
                Id = newPost.Id,
                Title = newPost.Title,
                Slug = newPost.Slug,
                Body = newPost.Body,
                CreatedDate = newPost.CreatedDate
            };
        }

        private static string GenerateSlug(string title)
        {
            return title.ToLower().Replace(" ", "-");
        }
    }
}
