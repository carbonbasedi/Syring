using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FaqModified_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs");

            migrationBuilder.AlterColumn<int>(
                name: "FaqCategoryId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Faqs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs",
                column: "FaqCategoryId",
                principalTable: "FaqCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Faqs");

            migrationBuilder.AlterColumn<int>(
                name: "FaqCategoryId",
                table: "Faqs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_FaqCategories_FaqCategoryId",
                table: "Faqs",
                column: "FaqCategoryId",
                principalTable: "FaqCategories",
                principalColumn: "Id");
        }
    }
}
