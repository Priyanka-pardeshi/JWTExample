using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//public static void Main(string[] args)
//{
    // Setup configuration
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

    // Setup DI
    var serviceProvider = new ServiceCollection()
        .AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("BlogDbContext")))
        .AddScoped<BlogService>() // Register other services as needed
        .BuildServiceProvider();

    // Use DbContext and services
    using (var scope = serviceProvider.CreateScope())
    {
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<BlogDbContext>();
        var blogService = services.GetRequiredService<BlogService>();

        // Now you can use dbContext and blogService as needed
        // For example:
        blogService.GetAllBlogs();
    }
//}