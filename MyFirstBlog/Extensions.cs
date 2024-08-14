using MyFirstBlog.Dtos;
using System;

namespace MyFirstBlog.Extensions
{
    public static class PostExtensions
    {
        public static void UpdatePost(this PostDto postDto, string title, string body, DateTime createDate)
        {
            postDto.Title = title;
            postDto.Body = body;
            postDto.CreatedDate = createDate; // Correct usage of CreateDate
        }
    }
}
