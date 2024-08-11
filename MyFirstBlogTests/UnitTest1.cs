using NUnit.Framework;
using Moq;
using MyFirstBlog.Controllers;
using MyFirstBlog.Dtos;
using MyFirstBlog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyFirstBlog.Entities;

[TestFixture]
public class PostsControllerTests
{
    private Mock<IPostService> mockPostService;
    private PostsController controller;

    [SetUp]
    public void Setup()
    {
        // Initialize the mock service
        mockPostService = new Mock<IPostService>();

        // Setup the mock to return a PostDto when CreatePostAsync is called
        mockPostService.Setup(service => service.CreatePostAsync(It.IsAny<Post>()))
                       .ReturnsAsync((Post post) => new PostDto
                       {
                           Id = post.Id,
                           Title = post.Title,
                           Slug = post.Slug,
                           Body = post.Body,
                           CreatedDate = post.CreatedDate
                       });

        // Pass the mock object to the controller
        controller = new PostsController(mockPostService.Object);
    }

    [Test]
    public async Task CreatePost_Returns201Created_WhenPostIsValid()
    {
        // Arrange
        var newPostDto = new PostDto { Title = "Valid Title", Body = "Some content" };

        // Act
        var result = await controller.CreatePost(newPostDto);

        // Assert
        Assert.IsInstanceOf<CreatedAtActionResult>(result);
        var createdAtActionResult = (CreatedAtActionResult)result;
        Assert.IsInstanceOf<PostDto>(createdAtActionResult.Value);
        var returnValue = (PostDto)createdAtActionResult.Value;
        Assert.AreEqual("Valid Title", returnValue.Title);
    }

    [Test]
    public async Task CreatePost_Returns400BadRequest_WhenTitleIsBlank()
    {
        // Arrange
        var newPostDto = new PostDto { Title = "", Body = "Some content" };
        controller.ModelState.AddModelError("Title", "Title cannot be blank");

        // Act
        var result = await controller.CreatePost(newPostDto);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = (BadRequestObjectResult)result;
        var serializableError = badRequestResult.Value as SerializableError;

        // Get the error messages for the "Title" key
        var titleErrors = serializableError["Title"] as string[];

        // Assert that the first error message contains the expected text
        Assert.That(titleErrors[0], Does.Contain("Title cannot be blank"));
    }

}
