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
			Console.WriteLine(responseString);
			var responseObject = JsonConvert.DeserializeObject<PostDto>(responseString);

			Assert.IsNotNull(responseObject);
			Assert.AreEqual(postData.Title, responseObject.Title);
			Assert.AreEqual(postData.Body, responseObject.Body);
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
			Assert.Contains("Title cannot be blank", responseObject.Errors);
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