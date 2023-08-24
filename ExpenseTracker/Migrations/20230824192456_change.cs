using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrys_Categories_CategoryID",
                table: "Entrys");

            migrationBuilder.DropIndex(
                name: "IX_Entrys_CategoryID",
                table: "Entrys");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Entrys");

            migrationBuilder.CreateIndex(
                name: "IX_Entrys_CatID",
                table: "Entrys",
                column: "CatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrys_Categories_CatID",
                table: "Entrys",
                column: "CatID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrys_Categories_CatID",
                table: "Entrys");

            migrationBuilder.DropIndex(
                name: "IX_Entrys_CatID",
                table: "Entrys");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Entrys",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entrys_CategoryID",
                table: "Entrys",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrys_Categories_CategoryID",
                table: "Entrys",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
