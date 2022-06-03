using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "museums",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_museums", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    registration_timestamp = table.Column<long>(type: "bigint", nullable: false),
                    last_edit_timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exhibitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    museum_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    start_timestamp = table.Column<long>(type: "bigint", nullable: false),
                    end_timestamp = table.Column<long>(type: "bigint", nullable: false),
                    cost = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitions", x => x.id);
                    table.ForeignKey(
                        name: "fk_exhibitions_museums_museum_id",
                        column: x => x.museum_id,
                        principalTable: "museums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exhibition_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exhibition_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    full_description = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibition_items", x => x.id);
                    table.ForeignKey(
                        name: "fk_exhibition_items_exhibitions_exhibition_id",
                        column: x => x.exhibition_id,
                        principalTable: "exhibitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    exhibition_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<double>(type: "bigint", nullable: false),
                    discount = table.Column<double>(type: "bigint", nullable: false),
                    creation_timestamp = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_exhibitions_exhibition_id",
                        column: x => x.exhibition_id,
                        principalTable: "exhibitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orders_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exhibition_items_exhibition_id",
                table: "exhibition_items",
                column: "exhibition_id");

            migrationBuilder.CreateIndex(
                name: "ix_exhibitions_museum_id",
                table: "exhibitions",
                column: "museum_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_exhibition_id",
                table: "orders",
                column: "exhibition_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_user_id",
                table: "orders",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exhibition_items");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "exhibitions");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "museums");
        }
    }
}
