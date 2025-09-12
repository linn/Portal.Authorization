using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Linn.Portal.Authorization.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class _1st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Action = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ScopeType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Sub = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Sub);
                });

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectSub = table.Column<Guid>(type: "uuid", nullable: true),
                    AssociatedResource = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Associations_Subjects_SubjectSub",
                        column: x => x.SubjectSub,
                        principalTable: "Subjects",
                        principalColumn: "Sub");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PrivilegeId = table.Column<int>(type: "integer", nullable: true),
                    AssociationId = table.Column<int>(type: "integer", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    GrantedById = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectSub = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Privileges_PrivilegeId",
                        column: x => x.PrivilegeId,
                        principalTable: "Privileges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Permissions_Subjects_GrantedById",
                        column: x => x.GrantedById,
                        principalTable: "Subjects",
                        principalColumn: "Sub",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Sub",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Subjects_SubjectSub",
                        column: x => x.SubjectSub,
                        principalTable: "Subjects",
                        principalColumn: "Sub");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Associations_SubjectSub",
                table: "Associations",
                column: "SubjectSub");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_AssociationId",
                table: "Permissions",
                column: "AssociationId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_GrantedById",
                table: "Permissions",
                column: "GrantedById");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PrivilegeId",
                table: "Permissions",
                column: "PrivilegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SubjectId",
                table: "Permissions",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SubjectSub",
                table: "Permissions",
                column: "SubjectSub");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
