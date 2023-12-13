using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Somativa.Migrations
{
    /// <inheritdoc />
    public partial class imagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "tbProdutos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "tbProdutos");
        }
    }
}
