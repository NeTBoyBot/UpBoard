using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpBoard.Host.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class addVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VerificationCode",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "User");
        }
    }
}
