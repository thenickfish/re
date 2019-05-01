using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Addresses_AddressId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_AddressId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Houses");

            migrationBuilder.AddColumn<int>(
                name: "HouseId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_HouseId",
                table: "Addresses",
                column: "HouseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Houses_HouseId",
                table: "Addresses",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Houses_HouseId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_HouseId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HouseId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Houses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_AddressId",
                table: "Houses",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Addresses_AddressId",
                table: "Houses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
