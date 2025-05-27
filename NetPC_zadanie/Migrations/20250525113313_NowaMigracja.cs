using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPC_zadanie.Migrations
{
    /// <inheritdoc />
    public partial class NowaMigracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Contacts");
        }
    }
}
