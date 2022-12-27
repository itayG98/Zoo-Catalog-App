using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class TryOneToManyComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { new Guid("2873a78e-21dd-4c7d-8aa7-47e1857bf9e0"), "Avian" },
                    { new Guid("37c53437-874d-4714-b7d1-c703043ee1a8"), "Mammal" }
                });

            migrationBuilder.InsertData(
                table: "Animel",
                columns: new[] { "ID", "BirthDate", "CategoryID", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { new Guid("0a1b87da-cd5a-48b6-bb1f-ffeceaf10b04"), new DateTime(2009, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2873a78e-21dd-4c7d-8aa7-47e1857bf9e0"), "", "~/Images/Defult.jpg", "Eagel" },
                    { new Guid("56966718-8663-4d9b-8bb6-05e053e75428"), new DateTime(2002, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("37c53437-874d-4714-b7d1-c703043ee1a8"), "", "~/Images/Defult.jpg", "Elephent" }
                });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "CommentId", "AnimelID", "Content" },
                values: new object[,]
                {
                    { new Guid("4b9e2e61-7a83-479a-b55a-c6babb1585c5"), new Guid("0a1b87da-cd5a-48b6-bb1f-ffeceaf10b04"), "Content" },
                    { new Guid("953994f9-295b-47a0-90c4-c2e9fa593833"), new Guid("56966718-8663-4d9b-8bb6-05e053e75428"), "Content" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Animel_CommentId",
                table: "Comment",
                column: "CommentId",
                principalTable: "Animel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Animel_CommentId",
                table: "Comment");

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("4b9e2e61-7a83-479a-b55a-c6babb1585c5"));

            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "CommentId",
                keyValue: new Guid("953994f9-295b-47a0-90c4-c2e9fa593833"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("0a1b87da-cd5a-48b6-bb1f-ffeceaf10b04"));

            migrationBuilder.DeleteData(
                table: "Animel",
                keyColumn: "ID",
                keyValue: new Guid("56966718-8663-4d9b-8bb6-05e053e75428"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("2873a78e-21dd-4c7d-8aa7-47e1857bf9e0"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryID",
                keyValue: new Guid("37c53437-874d-4714-b7d1-c703043ee1a8"));

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
    }
}
