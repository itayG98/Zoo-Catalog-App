using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("aba04f9e-7b86-46bb-8d9a-5dcf7c17dfd2"));

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("f6551e2a-3811-4695-99fd-db2340e5fc9d"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("490d7fe2-4f84-4b9f-b0af-5514918c7ed0"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("a5522b3b-dedb-46d0-869a-fc393a9ba597"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("1201df5a-7224-4d09-b24b-c6bf54410272"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("454cc411-e44d-4f97-b6d2-1b6d8a0f403b"));

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { new Guid("9aedc624-4b60-459a-84c2-246f8f91da6b"), "Mammal" },
                    { new Guid("9cb6f21a-2821-47d0-b3a9-b1bec34451e6"), "Avian" }
                });

            migrationBuilder.InsertData(
                table: "Animel",
                columns: new[] { "ID", "BirthDate", "CategoryID", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("3e2a4b8b-8b48-4074-aa49-99f51720e0e5"), new DateTime(2002, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9aedc624-4b60-459a-84c2-246f8f91da6b"), "", "~/Images/Defult.jpg", "Elephent" },
                    { new Guid("894f13d0-5ef6-43dc-be09-0fd47af00796"), new DateTime(2009, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9cb6f21a-2821-47d0-b3a9-b1bec34451e6"), "", "~/Images/Defult.jpg", "Eagel" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "AnimelID", "Content" },
                values: new object[,]
                {
                    { new Guid("30d9a3d3-c93b-437b-86e3-2327c0e5f4a6"), new Guid("3e2a4b8b-8b48-4074-aa49-99f51720e0e5"), "Content" },
                    { new Guid("bd0bf81b-2d86-47cf-84aa-6d75dcc21ab4"), new Guid("894f13d0-5ef6-43dc-be09-0fd47af00796"), "Content" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("30d9a3d3-c93b-437b-86e3-2327c0e5f4a6"));

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("bd0bf81b-2d86-47cf-84aa-6d75dcc21ab4"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("3e2a4b8b-8b48-4074-aa49-99f51720e0e5"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("894f13d0-5ef6-43dc-be09-0fd47af00796"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("9aedc624-4b60-459a-84c2-246f8f91da6b"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("9cb6f21a-2821-47d0-b3a9-b1bec34451e6"));

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { new Guid("1201df5a-7224-4d09-b24b-c6bf54410272"), "Avian" },
                    { new Guid("454cc411-e44d-4f97-b6d2-1b6d8a0f403b"), "Mammal" }
                });

            migrationBuilder.InsertData(
                table: "Animel",
                columns: new[] { "ID", "BirthDate", "CategoryID", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("490d7fe2-4f84-4b9f-b0af-5514918c7ed0"), new DateTime(2002, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("454cc411-e44d-4f97-b6d2-1b6d8a0f403b"), "", "~/Images/Defult.jpg", "Elephent" },
                    { new Guid("a5522b3b-dedb-46d0-869a-fc393a9ba597"), new DateTime(2009, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("1201df5a-7224-4d09-b24b-c6bf54410272"), "", "~/Images/Defult.jpg", "Eagel" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "AnimelID", "Content" },
                values: new object[,]
                {
                    { new Guid("aba04f9e-7b86-46bb-8d9a-5dcf7c17dfd2"), new Guid("a5522b3b-dedb-46d0-869a-fc393a9ba597"), "Content" },
                    { new Guid("f6551e2a-3811-4695-99fd-db2340e5fc9d"), new Guid("490d7fe2-4f84-4b9f-b0af-5514918c7ed0"), "Content" }
                });
        }
    }
}
