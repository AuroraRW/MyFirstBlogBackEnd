namespace MyFirstBlog.Entities;
public record Post {
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Slug { get; init; } = default!;
    public string Body { get; init; } = default!;
    public DateTime CreatedDate { get; init; }
    public string Description { get; init; } = default!;
}
