namespace MyFirstBlog.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }  // Updated to CreatedDate
    }
}
