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
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Build the application
var app = builder.Build();

// Map controllers to routes
app.MapControllers();

// Run the application
app.Run();

