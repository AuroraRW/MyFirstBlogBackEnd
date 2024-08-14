namespace MyFirstBlog.Helpers
{
    public static class DatabaseHelperBase
    {

        public static async Task ManageMigrationsAsync(IServiceProvider svcProvider)
        {
            //Service: An instance of db context
            var dbContextSvc = svcProvider.GetRequiredService<DataContext>();

            //Migration: This is the programmatic equivalent to Update-Database


        }
    }
}