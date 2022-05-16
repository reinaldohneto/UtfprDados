using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Utfpr.Dados.API.Migrations
{
    public partial class SolicitacaoProcessamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SolicitacaoProcessamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConjuntoDadosNome = table.Column<string>(type: "text", nullable: false),
                    ConjuntoDadosLink = table.Column<string>(type: "text", nullable: false),
                    LinkPrivadoBucket = table.Column<string>(type: "text", nullable: true),
                    ProcessamentoStatus = table.Column<int>(type: "integer", nullable: false),
                    OrganizacaoId = table.Column<Guid>(type: "uuid", nullable: false),
                    CadastradoEm = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoProcessamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoProcessamento_Organizacao_OrganizacaoId",
                        column: x => x.OrganizacaoId,
                        principalTable: "Organizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoProcessamento_OrganizacaoId",
                table: "SolicitacaoProcessamento",
                column: "OrganizacaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoProcessamento");
        }
    }
}
