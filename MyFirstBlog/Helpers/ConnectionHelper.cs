using System.Data.SqlClient;

namespace MyFirstBlog.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}
