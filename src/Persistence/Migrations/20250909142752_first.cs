using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Linn.Portal.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Subjects_SubjectSub",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Privileges_PrivilegeId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Subjects_SubjectSub",
                table: "Permissions");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "Privileges",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ScopeType",
                table: "Privileges",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectSub",
                table: "Permissions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "PrivilegeId",
                table: "Permissions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "AssociationId",
                table: "Permissions",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectSub",
                table: "Associations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "AssociatedResource",
                table: "Associations",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Associations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AssociationId",
                table: "Permissions",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Subjects_SubjectSub",
                table: "Associations",
                column: "SubjectSub",
                principalTable: "Subjects",
                principalColumn: "Sub");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Associations_AssociationId",
                table: "Permissions",
                column: "AssociationId",
                principalTable: "Associations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Privileges_PrivilegeId",
                table: "Permissions",
                column: "PrivilegeId",
                principalTable: "Privileges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Subjects_SubjectSub",
                table: "Permissions",
                column: "SubjectSub",
                principalTable: "Subjects",
                principalColumn: "Sub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Subjects_SubjectSub",
                table: "Associations");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Associations_AssociationId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Privileges_PrivilegeId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Subjects_SubjectSub",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_AssociationId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ScopeType",
                table: "Privileges");

            migrationBuilder.DropColumn(
                name: "AssociationId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Associations");

            migrationBuilder.AlterColumn<string>(
                name: "Action",
                table: "Privileges",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectSub",
                table: "Permissions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrivilegeId",
                table: "Permissions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubjectSub",
                table: "Associations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssociatedResource",
                table: "Associations",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Subjects_SubjectSub",
                table: "Associations",
                column: "SubjectSub",
                principalTable: "Subjects",
                principalColumn: "Sub",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Privileges_PrivilegeId",
                table: "Permissions",
                column: "PrivilegeId",
                principalTable: "Privileges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Subjects_SubjectSub",
                table: "Permissions",
                column: "SubjectSub",
                principalTable: "Subjects",
                principalColumn: "Sub",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
