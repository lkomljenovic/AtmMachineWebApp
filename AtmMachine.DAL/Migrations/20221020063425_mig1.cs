using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtmMachine.DAL.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountDetails",
                columns: new[] { "AccountNumber", "Balance" },
                values: new object[,]
                {
                    { "1122334455", 1234.56m },
                    { "1234567890", 5999.56m }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountNumber", "Pin" },
                values: new object[,]
                {
                    { "1122334455", "1234" },
                    { "1234567890", "5432" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountDetails",
                keyColumn: "AccountNumber",
                keyValue: "1122334455");

            migrationBuilder.DeleteData(
                table: "AccountDetails",
                keyColumn: "AccountNumber",
                keyValue: "1234567890");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountNumber",
                keyValue: "1122334455");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountNumber",
                keyValue: "1234567890");
        }
    }
}
