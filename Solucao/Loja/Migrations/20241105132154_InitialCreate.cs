using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaFornecedores",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Contato = table.Column<string>(type: "TEXT", nullable: false),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaFornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TabelaProdutos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    FornecedorId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaProdutos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaProdutos_TabelaFornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "TabelaFornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TabelaPedidos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ProdutoId = table.Column<string>(type: "TEXT", nullable: true),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TabelaPedidos_TabelaProdutos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "TabelaProdutos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TabelaPedidos_ProdutoId",
                table: "TabelaPedidos",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_TabelaProdutos_FornecedorId",
                table: "TabelaProdutos",
                column: "FornecedorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaPedidos");

            migrationBuilder.DropTable(
                name: "TabelaProdutos");

            migrationBuilder.DropTable(
                name: "TabelaFornecedores");
        }
    }
}
