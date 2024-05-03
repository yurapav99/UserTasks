using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserTasks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAssigmentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AssigmentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAssigmentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAssigmentHistories_Assignments_AssigmentId",
                        column: x => x.AssigmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAssigmentHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("b1da5222-fd46-487a-961d-7adbef71d6b1"), "Create project on Js", 0, null },
                    { new Guid("bda04c89-06d8-46ec-9ec7-6bf59cf56e95"), "Create project on Assembler", 2, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("9f32927f-e521-4e3e-965a-75f816ad8a10"), "Bob" },
                    { new Guid("af292b83-3a5a-41ae-bb4c-19b9263872c9"), "Jon" },
                    { new Guid("c309aadd-60c7-4d74-a212-9fab144421d9"), "Alice" }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "Name", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("94db15e2-0e2f-4e43-ae7b-de5b278499e2"), "Create project on C", 1, new Guid("c309aadd-60c7-4d74-a212-9fab144421d9") },
                    { new Guid("af921e72-5e35-4155-81e3-9030e5810faa"), "Create project on F#", 1, new Guid("af292b83-3a5a-41ae-bb4c-19b9263872c9") },
                    { new Guid("b871a972-4dbd-4fcf-8379-0a30c3e08b13"), "Create project on C#", 1, new Guid("9f32927f-e521-4e3e-965a-75f816ad8a10") },
                    { new Guid("dc75fadd-3c3f-4252-94de-37c6ad05b518"), "Create project on Ruby", 1, new Guid("c309aadd-60c7-4d74-a212-9fab144421d9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssigmentHistories_AssigmentId",
                table: "UserAssigmentHistories",
                column: "AssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssigmentHistories_UserId_AssigmentId",
                table: "UserAssigmentHistories",
                columns: new[] { "UserId", "AssigmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAssigmentHistories");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
