using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentApi.Migrations
{
    public partial class AtualizaTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendas_Vendedores_VendedorId",
                table: "Vendas");

            migrationBuilder.DropIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "VendedorId",
                table: "Vendas",
                newName: "IdVendedor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdVendedor",
                table: "Vendas",
                newName: "VendedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_VendedorId",
                table: "Vendas",
                column: "VendedorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendas_Vendedores_VendedorId",
                table: "Vendas",
                column: "VendedorId",
                principalTable: "Vendedores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
