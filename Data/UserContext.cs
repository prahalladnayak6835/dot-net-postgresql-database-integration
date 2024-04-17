using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    // Represents the database context for managing User entities
    public class UserContext : DbContext
    {
        // Constructor to initialize the context with provided options
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        // Represents the Users table in the database
        public DbSet<User> Users { get; set; }
    }
}
