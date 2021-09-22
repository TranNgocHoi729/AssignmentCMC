using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", nullable: false),
                    MobileNumber = table.Column<string>(type: "varchar(15)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailOptIn = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Email", "DOB", "EmailOptIn", "Gender", "MobileNumber", "Name", "Password" },
                values: new object[] { "CMC1@gmail.com", new DateTime(2021, 9, 22, 16, 46, 41, 372, DateTimeKind.Local).AddTicks(9663), null, 0, "0909090909", "NgokHoi", "f8cgamafgO7tEl6Y67qRrOK4JytFm3XIYxaHHhPwc74=|WkOOdSuKNVpzFOG9A3eGnA==" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Email", "DOB", "EmailOptIn", "Gender", "MobileNumber", "Name", "Password" },
                values: new object[] { "CMC2@gmail.com", new DateTime(2021, 9, 22, 16, 46, 41, 373, DateTimeKind.Local).AddTicks(8862), null, 0, "0909090999", "NgokHoi1234", "f8cgamafgO7tEl6Y67qRrOK4JytFm3XIYxaHHhPwc74=|WkOOdSuKNVpzFOG9A3eGnA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_MobileNumber",
                table: "Accounts",
                column: "MobileNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
