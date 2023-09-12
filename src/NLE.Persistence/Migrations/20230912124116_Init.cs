using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NLE.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "FileLogs",
                columns: table => new
                {
                    FileLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLine = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileLogs", x => x.FileLogId);
                });

            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.HostId);
                });

            migrationBuilder.CreateTable(
                name: "HttpMethods",
                columns: table => new
                {
                    HttpMethodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpMethods", x => x.HttpMethodId);
                });

            migrationBuilder.CreateTable(
                name: "Accesses",
                columns: table => new
                {
                    AccessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HttpMethodId = table.Column<int>(type: "int", nullable: false),
                    HostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    RemoteAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SentTo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Referer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accesses", x => x.AccessId);
                    table.ForeignKey(
                        name: "FK_Accesses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId");
                    table.ForeignKey(
                        name: "FK_Accesses_Hosts_HostId",
                        column: x => x.HostId,
                        principalTable: "Hosts",
                        principalColumn: "HostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accesses_HttpMethods_HttpMethodId",
                        column: x => x.HttpMethodId,
                        principalTable: "HttpMethods",
                        principalColumn: "HttpMethodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Name" },
                values: new object[,]
                {
                    { 1, "Not defined" },
                    { 2, "Chrome" },
                    { 3, "Safari" },
                    { 4, "Edge" },
                    { 5, "Firefox" }
                });

            migrationBuilder.InsertData(
                table: "HttpMethods",
                columns: new[] { "HttpMethodId", "Name" },
                values: new object[,]
                {
                    { 1, "HEAD" },
                    { 2, "OPTIONS" },
                    { 3, "GET" },
                    { 4, "POST" },
                    { 5, "PUT" },
                    { 6, "DELETE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_ClientId",
                table: "Accesses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_HostId",
                table: "Accesses",
                column: "HostId");

            migrationBuilder.CreateIndex(
                name: "IX_Accesses_HttpMethodId",
                table: "Accesses",
                column: "HttpMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accesses");

            migrationBuilder.DropTable(
                name: "FileLogs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "HttpMethods");
        }
    }
}
