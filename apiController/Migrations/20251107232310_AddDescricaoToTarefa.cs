using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiController.Migrations
{
    public partial class AddDescricaoToTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Tarefas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Tarefas");
        }
    }
}
