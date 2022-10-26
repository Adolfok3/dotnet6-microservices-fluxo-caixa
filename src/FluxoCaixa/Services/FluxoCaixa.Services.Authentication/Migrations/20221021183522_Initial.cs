using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FluxoCaixa.Services.Authentication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Phone = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Password = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedAt", "Email", "Fullname", "Password", "Phone", "Role", "Username" },
                values: new object[] { new Guid("9b8018a4-d92a-4821-9acc-cf7aa896be25"), new DateTime(1997, 10, 21, 18, 35, 21, 861, DateTimeKind.Utc).AddTicks(5828), new DateTime(2022, 10, 21, 18, 35, 21, 861, DateTimeKind.Utc).AddTicks(5849), "root@test.com", "root", "AQAAAAEAACcQAAAAEPck747NoF+LzBWfDLoLIND6Zxy0YgYkOWRpSf4mAnMahoDRqdHrue/dvw43yyDfRg==", "+5511900000000", "user", "root" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
