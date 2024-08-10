using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFirstBlog.Controllers;
using MyFirstBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace MyFirstBlog.Tests;

[TestClass]
public class PostsControllerTests
{
    [TestMethod]
    public void CreatePost_ShouldReturn201_WhenPostIsValid()
    {
        var controller = new PostsController();
        var result = controller.CreatePost(new PostDto { Title = "Test Title", Description = "Test Description" });

        Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
    }

    [TestMethod]
    public void CreatePost_ShouldReturn400_WhenTitleIsBlank()
    {
        var controller = new PostsController();
        var result = controller.CreatePost(new PostDto { Title = "", Description = "Test Description" });

        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }
}
