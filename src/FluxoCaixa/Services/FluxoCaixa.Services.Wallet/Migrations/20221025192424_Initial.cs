using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoCaixa.Services.Wallet.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(16,2)", precision: 16, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(16,2)", precision: 16, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Service = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    SellerName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Number",
                table: "Transactions",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
