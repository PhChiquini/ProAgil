using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repository.Migrations
{
    public partial class update_Campos_Evento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Eventos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Eventos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Eventos");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Eventos");
        }
    }
}
