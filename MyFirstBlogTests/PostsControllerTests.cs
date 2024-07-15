using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using MyFirstBlog.Dtos;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;

namespace MyFirstBlogTests
{
	[TestFixture]
	public class PostsControllerTests
	{
		private CustomWebApplicationFactory _factory;
		private HttpClient _client;

		[SetUp]
		public void Setup()
		{
			_factory = new CustomWebApplicationFactory();
			_client = _factory.CreateClient();
		}

		[Test]
		public async Task CreatePost_ValidData_ReturnsCreatedResponse()
		{
			// Arrange
			var postData = new CreatePostDto
			{
				Title = "Test Title",
				Body = "Test Content"
			};
			var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

			// Act
			var response = await _client.PostAsync("/posts", content);

			// Assert
			Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

			var responseString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonConvert.DeserializeObject<PostResponse>(responseString);

			Assert.IsNotNull(responseObject.Post);
			Assert.AreEqual(postData.Title, responseObject.Post.Title);
			Assert.AreEqual(postData.Body, responseObject.Post.Body);
			Assert.IsNotNull(responseObject.Post.Slug);
			Assert.IsNotNull(responseObject.Post.Id);
			Assert.That(responseObject.Post.CreatedDate, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
		}

		[Test]
		public async Task CreatePost_InvalidData_ReturnsBadRequestResponse()
		{
			// Arrange
			var postData = new CreatePostDto
			{
				Title = "",
				Body = "Test Content"
			};
			var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

			// Act
			var response = await _client.PostAsync("/posts", content);

			// Assert
			Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);

			var responseString = await response.Content.ReadAsStringAsync();
			var responseObject = JsonConvert.DeserializeObject<ErrorResponse>(responseString);

			Assert.IsNotNull(responseObject.Errors);
			Assert.That(responseObject.Errors, Contains.Item("Title cannot be blank"));
		}
	}

	public class PostResponse
	{
		public PostDto Post { get; set; }
	}

	public class ErrorResponse
	{
		public string[] Errors { get; set; }
	}
}