using Microsoft.EntityFrameworkCore.Migrations;

namespace SDLCSimulator_Data.Migrations
{
    public partial class addstaticseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Task",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Group",
                columns: new[] { "Id", "GroupName" },
                values: new object[,]
                {
                    { 1, "ПЗ-41" },
                    { 2, "ПЗ-42" },
                    { 3, "ПЗ-43" },
                    { 4, "ПЗ-44" },
                    { 5, "ПЗ-45" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "GroupId", "LastName", "Password", "Role" },
                values: new object[] { 2, "andriy.fomenko.pz@lpnu.ua", "Фоменко", null, "Андрій", "AEIatg7ShLybH2927m5UTtOGO2EjSBB6JuXbkhhhUHDIQAH+tKRvN81u9R7ZqhzOcA==", 1 });

            migrationBuilder.InsertData(
                table: "GroupTeacher",
                columns: new[] { "GroupId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "Description", "Difficulty", "ErrorRate", "MaxGrade", "Standard", "TeacherId", "Topic", "Type" },
                values: new object[] { 1, "{\"Columns\":[\"Функціональні\",\"Нефункціональні\"],\"Blocks\":[\"Пошук товарів за назвою\",\"Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу\",\"Перегляд вмісту кошику\",\"Вибір способу доставки\",\"Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень\",\"З’єднання з сайтом відбувається на основі протоколу HTTPS\",\"Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду\",\"Наявність вбудованого графічного редактору\",\"Наявність сторінки оформлення замовлення реклами\",\"Блокування облікового запису у разі підозрілої поведінки\"]}", 2, 1, 40, "{\"StandardOrResult\":{\"Функціональні\":[\"Перегляд вмісту кошику\",\"Вибір способу доставки\",\"Пошук товарів за назвою\",\"Наявність сторінки оформлення замовлення реклами\",\"Наявність вбудованого графічного редактору\"],\"Нефункціональні\":[\"З’єднання з сайтом відбувається на основі протоколу HTTPS\",\"Користувацький інтерфейс має відповідати 10 евристикам графічного інтерфейсу\",\"Програмний продукт має витримувати мінімум 100 запитів до серверу на секунду\",\"Блокування облікового запису у разі підозрілої поведінки\",\"Програмний продукт буде пов’язаний з поштовим клієнтом MS Outlook для отримання розсилки електронних повідомлень\"]}}", 2, "Вимоги до системи роботи магазину ювелірних виробів", 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "FirstName", "GroupId", "LastName", "Password", "Role" },
                values: new object[] { 1, "ivan.ivanov.pz.2018@lpnu.ua", "Іван", 1, "Іванов", "AKXwmMIdR9fEXaikvLavw33r0zyiXHBLBk4MJELb5RNwoyMCsi8NBf8advWXCTQ54A==", 0 });

            migrationBuilder.InsertData(
                table: "GroupTask",
                columns: new[] { "GroupId", "TaskId" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupTask",
                keyColumns: new[] { "GroupId", "TaskId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GroupTeacher",
                keyColumns: new[] { "GroupId", "TeacherId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GroupTeacher",
                keyColumns: new[] { "GroupId", "TeacherId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Group",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Topic",
                table: "Task",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
