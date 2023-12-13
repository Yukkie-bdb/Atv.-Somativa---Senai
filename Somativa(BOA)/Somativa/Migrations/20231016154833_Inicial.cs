using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Somativa.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbCategorias",
                columns: table => new
                {
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCategorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "tbClientes",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "tbCompras",
                columns: table => new
                {
                    CompraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCompras", x => x.CompraId);
                });

            migrationBuilder.CreateTable(
                name: "tbFornecedores",
                columns: table => new
                {
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbFornecedores", x => x.FornecedorId);
                });

            migrationBuilder.CreateTable(
                name: "tbVendas",
                columns: table => new
                {
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbVendas", x => x.VendaId);
                    table.ForeignKey(
                        name: "FK_tbVendas_tbClientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "tbClientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbProdutos",
                columns: table => new
                {
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProdutos", x => x.ProdutoId);
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbCategorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "tbCategorias",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbProdutos_tbFornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "tbFornecedores",
                        principalColumn: "FornecedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbCompraItens",
                columns: table => new
                {
                    CompraItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompraId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCompraItens", x => x.CompraItemId);
                    table.ForeignKey(
                        name: "FK_tbCompraItens_tbCompras_CompraId",
                        column: x => x.CompraId,
                        principalTable: "tbCompras",
                        principalColumn: "CompraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbCompraItens_tbProdutos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProdutos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbVendaItens",
                columns: table => new
                {
                    VendaItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VendaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbVendaItens", x => x.VendaItemId);
                    table.ForeignKey(
                        name: "FK_tbVendaItens_tbProdutos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "tbProdutos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbVendaItens_tbVendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "tbVendas",
                        principalColumn: "VendaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbCompraItens_CompraId",
                table: "tbCompraItens",
                column: "CompraId");

            migrationBuilder.CreateIndex(
                name: "IX_tbCompraItens_ProdutoId",
                table: "tbCompraItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_CategoriaId",
                table: "tbProdutos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_FornecedorId",
                table: "tbProdutos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbVendaItens_ProdutoId",
                table: "tbVendaItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_tbVendaItens_VendaId",
                table: "tbVendaItens",
                column: "VendaId");

            migrationBuilder.CreateIndex(
                name: "IX_tbVendas_ClienteId",
                table: "tbVendas",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCompraItens");

            migrationBuilder.DropTable(
                name: "tbVendaItens");

            migrationBuilder.DropTable(
                name: "tbCompras");

            migrationBuilder.DropTable(
                name: "tbProdutos");

            migrationBuilder.DropTable(
                name: "tbVendas");

            migrationBuilder.DropTable(
                name: "tbCategorias");

            migrationBuilder.DropTable(
                name: "tbFornecedores");

            migrationBuilder.DropTable(
                name: "tbClientes");
        }
    }
}
