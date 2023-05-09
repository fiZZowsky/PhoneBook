using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhoneBook_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedContactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirhDate", "CreatedDate", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1989, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 9, 19, 0, 58, 583, DateTimeKind.Local).AddTicks(539), "example1@email.com", "Jan", "Kowalski", "password", "123456789", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 9, 19, 0, 58, 583, DateTimeKind.Local).AddTicks(582), "example2@mail.com", "Anna", "Nowak", "password2", "987654321", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
