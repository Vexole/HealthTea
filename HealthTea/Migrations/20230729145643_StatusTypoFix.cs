using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthTea.Migrations
{
    /// <inheritdoc />
    public partial class StatusTypoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Staus",
                table: "Teas",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Teas",
                newName: "Staus");
        }
    }
}
