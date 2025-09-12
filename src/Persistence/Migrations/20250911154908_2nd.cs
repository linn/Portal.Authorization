using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linn.Portal.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _2nd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Subjects_SubjectId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_SubjectId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Permissions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Permissions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SubjectId",
                table: "Permissions",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Subjects_SubjectId",
                table: "Permissions",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Sub",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
