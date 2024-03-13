using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WowBravoFunkyRap.Model.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PublicityImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PublicityImageType = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "NVARCHAR(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    ImageTitle = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    ImageAlt = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    ImageName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    ImageNameSm = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    ImageNameXs = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    StartTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DisplaySeq = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicityImages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    DisplaySeq = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Account = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: true),
                    FirstName = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DisplaySeq = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdateUserId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleItem",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleItem", x => new { x.Id, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleItem_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreateTime", "CreateUserId", "DisplaySeq", "Name", "UpdateTime", "UpdateUserId" },
                values: new object[,]
                {
                    { new Guid("30f11ae8-2673-4f56-9a61-ed34572d2655"), null, null, 4, "使用者查詢", null, null },
                    { new Guid("3c00b351-6219-45df-b422-64af224f3641"), null, null, 3, "使用者設定", null, null },
                    { new Guid("74e10ab9-6b46-4306-b859-a41183354714"), null, null, 6, "角色查詢", null, null },
                    { new Guid("8de9e40f-2b8a-4d7b-a8a0-19c050aed285"), null, null, 1, "宣傳圖片設定", null, null },
                    { new Guid("b6955c4c-95b2-4651-ac1b-a9eeac3667f1"), null, null, 2, "宣傳圖片查詢", null, null },
                    { new Guid("dd5f8b00-94f9-4577-b5a5-dbabfd813a8a"), null, null, 5, "角色設定", null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "CreateTime", "CreateUserId", "DisplaySeq", "Email", "FirstName", "IsEnabled", "LastName", "PasswordHash", "UpdateTime", "UpdateUserId" },
                values: new object[,]
                {
                    { new Guid("52e02831-fdf2-4eb8-a7ff-314d53f010dd"), "joanne", null, null, 0, "wwjoannems@gmail.com", "Joanne", true, "Wang", "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", null, null },
                    { new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a"), "walter", null, null, 0, "smickey33@gmail.com", "Walter", true, "Chen", "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", null, null }
                });

            migrationBuilder.InsertData(
                table: "RoleItem",
                columns: new[] { "Id", "RoleId" },
                values: new object[,]
                {
                    { "PublicityImageRead", new Guid("b6955c4c-95b2-4651-ac1b-a9eeac3667f1") },
                    { "PublicityImageWrite", new Guid("8de9e40f-2b8a-4d7b-a8a0-19c050aed285") },
                    { "RoleRead", new Guid("74e10ab9-6b46-4306-b859-a41183354714") },
                    { "RoleWrite", new Guid("dd5f8b00-94f9-4577-b5a5-dbabfd813a8a") },
                    { "UserRead", new Guid("30f11ae8-2673-4f56-9a61-ed34572d2655") },
                    { "UserWrite", new Guid("3c00b351-6219-45df-b422-64af224f3641") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("30f11ae8-2673-4f56-9a61-ed34572d2655"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") },
                    { new Guid("3c00b351-6219-45df-b422-64af224f3641"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") },
                    { new Guid("74e10ab9-6b46-4306-b859-a41183354714"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") },
                    { new Guid("8de9e40f-2b8a-4d7b-a8a0-19c050aed285"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") },
                    { new Guid("b6955c4c-95b2-4651-ac1b-a9eeac3667f1"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") },
                    { new Guid("dd5f8b00-94f9-4577-b5a5-dbabfd813a8a"), new Guid("f50da497-7558-4b3d-b51a-b000ccd5f99a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleItem_RoleId",
                table: "RoleItem",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicityImages");

            migrationBuilder.DropTable(
                name: "RoleItem");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
