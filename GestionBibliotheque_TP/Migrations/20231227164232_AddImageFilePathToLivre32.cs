using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionBibliotheque_TP.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFilePathToLivre32 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageData",
                table: "Livre",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Livre",
                newName: "ImageData");
        }
    }
}
