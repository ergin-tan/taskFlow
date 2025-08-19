using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTaskHistoryWithAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskHistories");

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TableName = table.Column<string>(type: "text", nullable: false),
                    Action = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    OldValues = table.Column<string>(type: "text", nullable: false),
                    NewValues = table.Column<string>(type: "text", nullable: false),
                    PrimaryKey = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 154, DateTimeKind.Utc).AddTicks(586), new DateTime(2025, 8, 18, 8, 21, 24, 154, DateTimeKind.Utc).AddTicks(1075) });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 154, DateTimeKind.Utc).AddTicks(1520), new DateTime(2025, 8, 18, 8, 21, 24, 154, DateTimeKind.Utc).AddTicks(1521) });

            migrationBuilder.UpdateData(
                table: "TaskAssignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 910, DateTimeKind.Utc).AddTicks(332), new DateTime(2025, 8, 23, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(9844), new DateTime(2025, 8, 18, 8, 21, 24, 910, DateTimeKind.Utc).AddTicks(333) });

            migrationBuilder.UpdateData(
                table: "TaskAssignments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 910, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 9, 2, 8, 21, 24, 910, DateTimeKind.Utc).AddTicks(343), new DateTime(2025, 8, 18, 8, 21, 24, 910, DateTimeKind.Utc).AddTicks(347) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 8, 28, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(1620), new DateTime(2025, 8, 18, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(6213) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(6264), new DateTime(2025, 9, 7, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(6231), new DateTime(2025, 8, 18, 8, 21, 24, 909, DateTimeKind.Utc).AddTicks(6266) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 560, DateTimeKind.Utc).AddTicks(3725), "$2a$11$zfPL5d/blN8EMDzEnVaS2eZKvxQ02yvg.y1vRA/RVdcz4zJeu0NLi", new DateTime(2025, 8, 18, 8, 21, 24, 560, DateTimeKind.Utc).AddTicks(3740) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 18, 8, 21, 24, 908, DateTimeKind.Utc).AddTicks(3217), "$2a$11$QdvCxidMVpPqLTj/Vfgd.OzIXDnzM/2PvT8.oxOcKBGiIGUWLJf2m", new DateTime(2025, 8, 18, 8, 21, 24, 908, DateTimeKind.Utc).AddTicks(3231) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.CreateTable(
                name: "TaskHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChangedByUserId = table.Column<int>(type: "integer", nullable: false),
                    WorkTaskId = table.Column<int>(type: "integer", nullable: false),
                    ChangeDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NewStatus = table.Column<string>(type: "text", nullable: false),
                    OldStatus = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Tasks_WorkTaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistories_Users_ChangedByUserId",
                        column: x => x.ChangedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 38, 839, DateTimeKind.Utc).AddTicks(4944), new DateTime(2025, 8, 9, 8, 48, 38, 839, DateTimeKind.Utc).AddTicks(5238) });

            migrationBuilder.UpdateData(
                table: "Offices",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 38, 839, DateTimeKind.Utc).AddTicks(5460), new DateTime(2025, 8, 9, 8, 48, 38, 839, DateTimeKind.Utc).AddTicks(5461) });

            migrationBuilder.UpdateData(
                table: "TaskAssignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9909), new DateTime(2025, 8, 14, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9624), new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9910) });

            migrationBuilder.UpdateData(
                table: "TaskAssignments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9919), new DateTime(2025, 8, 24, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9917), new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(9919) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(7505), new DateTime(2025, 8, 19, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(6137), new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(7507) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "DueDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(7530), new DateTime(2025, 8, 29, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(7523), new DateTime(2025, 8, 9, 8, 48, 39, 446, DateTimeKind.Utc).AddTicks(7531) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 185, DateTimeKind.Utc).AddTicks(4783), "$2a$11$puHr7L.eiAl.e28fYx8TEOF.TTrAmFDBrPN4I97wn/nBxrm1xEv..", new DateTime(2025, 8, 9, 8, 48, 39, 185, DateTimeKind.Utc).AddTicks(4793) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 9, 8, 48, 39, 445, DateTimeKind.Utc).AddTicks(7512), "$2a$11$RwYWtuLHZt9gtA3i3xqRHOlrFQFXPpqoz5fybTyf2r3gqWtkaCJhK", new DateTime(2025, 8, 9, 8, 48, 39, 445, DateTimeKind.Utc).AddTicks(7522) });

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_ChangedByUserId",
                table: "TaskHistories",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_WorkTaskId",
                table: "TaskHistories",
                column: "WorkTaskId");
        }
    }
}
