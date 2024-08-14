namespace MyFirstBlog.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using MyFirstBlog.Entities;
    using Microsoft.Extensions.Configuration;

    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(ConnectionHelper.GetConnectionString(Configuration));
        }

        public DbSet<Post> Posts { get; set; }
    }
}
