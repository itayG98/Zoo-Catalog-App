using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Model.Migrations
{
    /// <inheritdoc />
    public partial class SecondInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Animel",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animel_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AnimelID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comment_Animel_AnimelID",
                        column: x => x.AnimelID,
                        principalTable: "Animel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Animel_CategoryID",
                table: "Animel",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AnimelID",
                table: "Comment",
                column: "AnimelID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Animel");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
