using Microsoft.EntityFrameworkCore.Migrations;

namespace SDLCSimulator_Data.Migrations
{
    public partial class Addadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "GroupId", "LastName", "Password", "Role" },
                values: new object[] { 3, "сергій.федецький.адмін@lpnu.ua", "Сергій", null, "Федецький", "AGiprihD8YNNbQk2w5XdYqNtbOxu8Qly+7gmJloWjaKPdPWSAHIb2SAbMsaRO08e6Q==", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
