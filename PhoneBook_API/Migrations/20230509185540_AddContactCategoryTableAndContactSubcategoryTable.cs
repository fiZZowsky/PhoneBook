using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhoneBook_API.Migrations
{
    /// <inheritdoc />
    public partial class AddContactCategoryTableAndContactSubcategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "BirhDate",
                table: "Contacts",
                newName: "BirthDate");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Subcategory",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ContactCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactSubcategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSubcategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactSubcategories_ContactCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ContactCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactSubcategories_CategoryId",
                table: "ContactSubcategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_ContactCategories_CategoryId",
                table: "Contacts",
                column: "CategoryId",
                principalTable: "ContactCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_ContactCategories_CategoryId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "ContactSubcategories");

            migrationBuilder.DropTable(
                name: "ContactCategories");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Subcategory",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Contacts",
                newName: "BirhDate");

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirhDate", "CreatedDate", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1989, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 9, 19, 0, 58, 583, DateTimeKind.Local).AddTicks(539), "example1@email.com", "Jan", "Kowalski", "password", "123456789", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1995, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 9, 19, 0, 58, 583, DateTimeKind.Local).AddTicks(582), "example2@mail.com", "Anna", "Nowak", "password2", "987654321", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }
    }
}
