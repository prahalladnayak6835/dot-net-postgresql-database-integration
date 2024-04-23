// Import necessary namespaces
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// Create a new web application builder
var builder = WebApplication.CreateBuilder(args);

// Add controllers to the services collection
builder.Services.AddControllers();

// Get the configuration from the builder
var configuration = builder.Configuration;

// Add the UserContext to the services collection using Npgsql for PostgreSQL
builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection"))
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

// Build the application
var app = builder.Build();

// Create a scope for accessing services
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Get the UserContext service required for database operations
        var context = services.GetRequiredService<UserContext>();
        // Apply pending migrations to the database
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // If an exception occurs during migration, log the error
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();

