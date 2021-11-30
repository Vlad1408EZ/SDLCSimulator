using Microsoft.EntityFrameworkCore.Migrations;

namespace SDLCSimulator_Data.Migrations
{
    public partial class AddTimeSupportForTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskTime",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1,
                column: "TaskTime",
                value: 300);

            migrationBuilder.UpdateData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2,
                column: "TaskTime",
                value: 450);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskTime",
                table: "Task");
        }
    }
}
