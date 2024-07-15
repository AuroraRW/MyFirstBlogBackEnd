using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyFirstBlog.Helpers;
using System.Linq;

namespace MyFirstBlogTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.ConfigureServices(services =>
		{
			var descriptor = services.FirstOrDefault(
				d => d.ServiceType ==
					typeof(DbContextOptions<DataContext>));

			if (descriptor != null)
			{
				services.Remove(descriptor);
			}

			services.AddDbContext<DataContext>(options =>
			{
				options.UseInMemoryDatabase("InMemoryDbForTesting");
			});
		});
	}
}