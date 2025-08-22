using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hahn.Migrations
{
    /// <inheritdoc />
    public partial class boolAuthCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "authCompeleted",
                table: "Users",
                newName: "AuthCompleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthCompleted",
                table: "Users",
                newName: "authCompeleted");
        }
    }
}
