using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    // Represents a user entity in the database
    // Specifies the table name in the database
    [Table("users")] 
    public class User
    {
        // Specifies the column name in the database
        [Column("id")] 
        // Gets or sets the user ID
        public int Id { get; set; } 
        // Specifies the column name in the database

        [Column("name")] 
        // Gets or sets the user name
        public string Name { get; set; } 
        // Specifies the column name in the database

        [Column("email")] 
        // Gets or sets the user email
        public string Email { get; set; } 
    }
}
