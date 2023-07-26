using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FaqAndFaqCategoryModified_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_CategoryId",
                table: "Faqs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_FaqCategories_CategoryId",
                table: "Faqs",
                column: "CategoryId",
                principalTable: "FaqCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_FaqCategories_CategoryId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_CategoryId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Faqs");
        }
    }
}
