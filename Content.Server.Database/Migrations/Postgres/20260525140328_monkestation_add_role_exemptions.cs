using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Content.Server.Database.Migrations.Postgres
{
    /// <inheritdoc />
    public partial class monkestation_add_role_exemptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monkestation_role_time_exemptions",
                columns: table => new
                {
                    monkestation_role_time_exemptions_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monkestation_role_time_exemptions", x => x.monkestation_role_time_exemptions_id);
                });

            migrationBuilder.CreateTable(
                name: "monkestation_role_time_exemption_roles",
                columns: table => new
                {
                    monkestation_role_time_exemption_roles_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_type = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: false),
                    exemption_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monkestation_role_time_exemption_roles", x => x.monkestation_role_time_exemption_roles_id);
                    table.ForeignKey(
                        name: "FK_monkestation_role_time_exemption_roles_monkestation_role_ti~",
                        column: x => x.exemption_id,
                        principalTable: "monkestation_role_time_exemptions",
                        principalColumn: "monkestation_role_time_exemptions_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_monkestation_role_time_exemption_roles_exemption_id",
                table: "monkestation_role_time_exemption_roles",
                column: "exemption_id");

            migrationBuilder.CreateIndex(
                name: "IX_monkestation_role_time_exemption_roles_role_type_role_id_ex~",
                table: "monkestation_role_time_exemption_roles",
                columns: new[] { "role_type", "role_id", "exemption_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_monkestation_role_time_exemptions_user_id",
                table: "monkestation_role_time_exemptions",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monkestation_role_time_exemption_roles");

            migrationBuilder.DropTable(
                name: "monkestation_role_time_exemptions");
        }
    }
}
