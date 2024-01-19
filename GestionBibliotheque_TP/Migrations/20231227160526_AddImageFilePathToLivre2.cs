using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionBibliotheque_TP.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFilePathToLivre2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Livre",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Livre");
        }
    }
}
