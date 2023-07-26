using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FaqAndFaqCategoryModified_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_FaqCategoryId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "FaqCategoryId",
                table: "Faqs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FaqCategoryId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_FaqCategoryId",
                table: "Faqs",
                column: "FaqCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs",
                column: "FaqCategoryId",
                principalTable: "FaqCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
