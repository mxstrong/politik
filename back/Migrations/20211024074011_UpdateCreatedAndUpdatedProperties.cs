using Microsoft.EntityFrameworkCore.Migrations;

namespace Politics.Migrations
{
    public partial class UpdateCreatedAndUpdatedProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Politicians",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Politicians",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Parties",
                newName: "UpdatedById");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Parties",
                newName: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_CreatedById",
                table: "Politicians",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_UpdatedById",
                table: "Politicians",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_CreatedById",
                table: "Parties",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_UpdatedById",
                table: "Parties",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Users_CreatedById",
                table: "Parties",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Users_UpdatedById",
                table: "Parties",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Politicians_Users_CreatedById",
                table: "Politicians",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Politicians_Users_UpdatedById",
                table: "Politicians",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Users_CreatedById",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Users_UpdatedById",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_Politicians_Users_CreatedById",
                table: "Politicians");

            migrationBuilder.DropForeignKey(
                name: "FK_Politicians_Users_UpdatedById",
                table: "Politicians");

            migrationBuilder.DropIndex(
                name: "IX_Politicians_CreatedById",
                table: "Politicians");

            migrationBuilder.DropIndex(
                name: "IX_Politicians_UpdatedById",
                table: "Politicians");

            migrationBuilder.DropIndex(
                name: "IX_Parties_CreatedById",
                table: "Parties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_UpdatedById",
                table: "Parties");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Politicians",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Politicians",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Parties",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Parties",
                newName: "CreatedBy");
        }
    }
}
