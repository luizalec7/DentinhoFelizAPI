using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentinhoFeliz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALARME",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    HORARIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    MENSAGEM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALARME", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DUVIDA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PERGUNTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RESPOSTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DUVIDA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "QUIZ",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PERGUNTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    OPCOES = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    RESPOSTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QUIZ", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALARME");

            migrationBuilder.DropTable(
                name: "DUVIDA");

            migrationBuilder.DropTable(
                name: "QUIZ");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
