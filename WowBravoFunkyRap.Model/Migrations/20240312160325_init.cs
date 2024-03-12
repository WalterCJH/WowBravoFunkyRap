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
                    { new Guid("1429d0ca-c012-4467-a0ce-40c345ea7860"), null, null, 2, "宣傳圖片查詢", null, null },
                    { new Guid("2196a47c-4ada-46b5-a600-0d0f7ce08349"), null, null, 4, "使用者查詢", null, null },
                    { new Guid("7d8d8453-f096-4c15-942e-900fde46ad37"), null, null, 5, "角色設定", null, null },
                    { new Guid("8f904dc0-2318-4e14-878a-7ff1897a6b5f"), null, null, 1, "宣傳圖片設定", null, null },
                    { new Guid("99fd5995-55b2-4625-8203-fe60ded5a252"), null, null, 3, "使用者設定", null, null },
                    { new Guid("a5a53305-d5d0-49b8-9d55-d72d1da6f690"), null, null, 6, "角色查詢", null, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "CreateTime", "CreateUserId", "DisplaySeq", "Email", "FirstName", "IsEnabled", "LastName", "PasswordHash", "UpdateTime", "UpdateUserId" },
                values: new object[,]
                {
                    { new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71"), "walter", null, null, 0, "smickey33@gmail.com", "Walter", true, "Chen", "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", null, null },
                    { new Guid("598788df-0c19-4f1e-b349-ab982a2b9981"), "joanne", null, null, 0, "wwjoannems@gmail.com", "Joanne", true, "Wang", "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", null, null }
                });

            migrationBuilder.InsertData(
                table: "RoleItem",
                columns: new[] { "Id", "RoleId" },
                values: new object[,]
                {
                    { "PublicityImageRead", new Guid("1429d0ca-c012-4467-a0ce-40c345ea7860") },
                    { "PublicityImageWrite", new Guid("8f904dc0-2318-4e14-878a-7ff1897a6b5f") },
                    { "RoleRead", new Guid("a5a53305-d5d0-49b8-9d55-d72d1da6f690") },
                    { "RoleWrite", new Guid("7d8d8453-f096-4c15-942e-900fde46ad37") },
                    { "UserRead", new Guid("2196a47c-4ada-46b5-a600-0d0f7ce08349") },
                    { "UserWrite", new Guid("99fd5995-55b2-4625-8203-fe60ded5a252") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1429d0ca-c012-4467-a0ce-40c345ea7860"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") },
                    { new Guid("2196a47c-4ada-46b5-a600-0d0f7ce08349"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") },
                    { new Guid("7d8d8453-f096-4c15-942e-900fde46ad37"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") },
                    { new Guid("8f904dc0-2318-4e14-878a-7ff1897a6b5f"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") },
                    { new Guid("99fd5995-55b2-4625-8203-fe60ded5a252"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") },
                    { new Guid("a5a53305-d5d0-49b8-9d55-d72d1da6f690"), new Guid("5077c42f-8859-48c2-97e1-5d5a2e12fb71") }
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
