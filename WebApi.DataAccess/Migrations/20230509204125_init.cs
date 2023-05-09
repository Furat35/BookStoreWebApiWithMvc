using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "User");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "Editor");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("651e4c92-91ab-4956-9427-3da18065b9c7"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEJqHw7bgWTVkJo3uX5lnWEmyyCZjG7Egxsv6AXbMGEoBI/QxWzOXBghlfO3NkZrwYA==");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2496de42-94ef-4afd-a575-241510d88294"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6019));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d129c85a-d830-4d67-be09-712dc444c794"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6026));

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: new Guid("b36dc350-66c5-4567-bc58-572d1821b7b8"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6666));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1c2ee0bd-8f81-4cce-8891-13d28c982455"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6356));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d132c0a2-4694-4390-84b2-10f7c8cd90fa"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ecdd8f0a-5f41-41ce-b732-f269a453b110"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(6963));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0a66353-7e36-457f-b6b0-8baa1df6029a"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 41, 24, 595, DateTimeKind.Local).AddTicks(7332));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1ba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("9ba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "user");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bba776bf-3d61-467a-9002-3a259e5d09d5"),
                column: "Name",
                value: "editor");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("651e4c92-91ab-4956-9427-3da18065b9c7"),
                column: "PasswordHash",
                value: "AQAAAAEAACcQAAAAEAx0Hm6DlULaAN7bDENogvnKtiz2o9y3LOp4eHGW4RugBdLC0OVUGJaukYt+xgH4ig==");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("2496de42-94ef-4afd-a575-241510d88294"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(3267));

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("d129c85a-d830-4d67-be09-712dc444c794"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(3274));

            migrationBuilder.UpdateData(
                table: "BookGenres",
                keyColumn: "Id",
                keyValue: new Guid("b36dc350-66c5-4567-bc58-572d1821b7b8"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(4114));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1c2ee0bd-8f81-4cce-8891-13d28c982455"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(3741));

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d132c0a2-4694-4390-84b2-10f7c8cd90fa"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(3728));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ecdd8f0a-5f41-41ce-b732-f269a453b110"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(4542));

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0a66353-7e36-457f-b6b0-8baa1df6029a"),
                column: "CreateDate",
                value: new DateTime(2023, 5, 9, 23, 37, 15, 231, DateTimeKind.Local).AddTicks(4922));
        }
    }
}
