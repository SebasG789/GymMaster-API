using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class RepeticionesToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Repeticiones",
                table: "Ejercicios",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Repeticiones",
                table: "Ejercicios",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
