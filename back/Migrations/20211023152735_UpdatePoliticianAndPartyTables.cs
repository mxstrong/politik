using Microsoft.EntityFrameworkCore.Migrations;

namespace Politics.Migrations
{
    public partial class UpdatePoliticianAndPartyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Politicians",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Partyid",
                table: "Parties",
                newName: "PartyId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Parties",
                newName: "ShortName");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Politicians",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Politicians",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LongName",
                table: "Parties",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Politicians");

            migrationBuilder.DropColumn(
                name: "LongName",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Politicians",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "PartyId",
                table: "Parties",
                newName: "Partyid");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                table: "Parties",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Politicians",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
