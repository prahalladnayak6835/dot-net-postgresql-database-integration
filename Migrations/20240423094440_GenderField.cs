using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserCrudApi.Migrations
{
    /// <inheritdoc />
    public partial class GenderField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "gender",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
