using System;
using System.ComponentModel.DataAnnotations;

namespace MyFirstBlog.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreatedDate { get; set; }
    }

    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreatedDate { get; set; }
    }
}
