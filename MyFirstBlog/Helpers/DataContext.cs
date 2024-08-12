namespace MyFirstBlog.Helpers;

using Microsoft.EntityFrameworkCore;
using MyFirstBlog.Entities;


public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = ConnectionHelper.GetConnectionString(Configuration); //delete later
        Console.WriteLine($"Using connection string: {connectionString}"); //delete later
        options.UseNpgsql(ConnectionHelper.GetConnectionString(Configuration));
    }

    public DbSet<Post> Posts { get; set; }
}