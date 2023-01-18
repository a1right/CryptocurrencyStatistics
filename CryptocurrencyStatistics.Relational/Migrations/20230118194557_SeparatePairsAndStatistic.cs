using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CryptocurrencyStatistics.RelationalStorage.Migrations
{
    public partial class SeparatePairsAndStatistic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.CreateTable(
                name: "Pair",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pair_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pair", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Statistic_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PairId = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<decimal>(type: "numeric(13,8)", nullable: false),
                    created_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistic_data", x => x.id);
                    table.ForeignKey(
                        name: "FK_Statistic_data_Pair_PairId",
                        column: x => x.PairId,
                        principalTable: "Pair",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uk_pair_name",
                table: "Pair",
                column: "pair_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_record_created_datetime",
                table: "Statistic_data",
                column: "created_datetime");

            migrationBuilder.CreateIndex(
                name: "IX_Statistic_data_PairId",
                table: "Statistic_data",
                column: "PairId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistic_data");

            migrationBuilder.DropTable(
                name: "Pair");

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    pair_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    value = table.Column<decimal>(type: "numeric(13,8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_record_created_datetime",
                table: "Record",
                column: "created_datetime");
        }
    }
}
