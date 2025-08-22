using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hahn.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthCompletedToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "authCompeleted",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authCompeleted",
                table: "Users");
        }
    }
}
