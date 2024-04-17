using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    // Represents a user entity in the database
    [Table("users")] // Specifies the table name in the database
    public class User
    {
        [Column("id")] // Specifies the column name in the database
        public int Id { get; set; } // Gets or sets the user ID

        [Column("name")] // Specifies the column name in the database
        public string Name { get; set; } // Gets or sets the user name

        [Column("email")] // Specifies the column name in the database
        public string Email { get; set; } // Gets or sets the user email
    }
}
