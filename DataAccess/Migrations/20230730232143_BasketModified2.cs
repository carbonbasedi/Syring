using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BasketModified2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_AspNetUsers_IdentityUserId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_IdentityUserId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Baskets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Baskets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_IdentityUserId",
                table: "Baskets",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_AspNetUsers_IdentityUserId",
                table: "Baskets",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
