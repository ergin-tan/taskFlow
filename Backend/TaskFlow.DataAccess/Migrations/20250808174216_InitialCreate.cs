using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskFlow.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OfficeName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmployeeID = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    OfficeId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    AssignedBy = table.Column<int>(type: "integer", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    CurrentStatus = table.Column<string>(type: "text", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssignedBy",
                        column: x => x.AssignedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkTaskId = table.Column<int>(type: "integer", nullable: false),
                    AssignedTo = table.Column<int>(type: "integer", nullable: false),
                    AssignedPart = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Tasks_WorkTaskId",
                        column: x => x.WorkTaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskAssignments_Users_AssignedTo",
                        column: x => x.AssignedTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkTaskId = table.Column<int>(type: "integer", nullable: false),
                    ChangedByUserId = table.Column<int>(type: "integer", nullable: false),
                    OldStatus = table.Column<string>(type: "text", nullable: true),
                    NewStatus = table.Column<string>(type: "text", nullable: false),
                    ChangeDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "CreatedAt", "OfficeName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 8, 17, 42, 12, 290, DateTimeKind.Utc).AddTicks(9987), "Office1", new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(401) },
                    { 2, new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(746), "Office2", new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(747) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "EmployeeID", "FirstName", "IsActive", "LastName", "OfficeId", "PasswordHash", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 8, 17, 42, 12, 693, DateTimeKind.Utc).AddTicks(7355), "123456", "John", true, "Doe", 1, "$2a$11$eLmtnQ7l3hm2ROz6UzcfLOE9JZnyWzCduGPvqLVL9ITQfTzvRyCXe", "DepartmentHead", new DateTime(2025, 8, 8, 17, 42, 12, 693, DateTimeKind.Utc).AddTicks(7366) },
                    { 2, new DateTime(2025, 8, 8, 17, 42, 13, 7, DateTimeKind.Utc).AddTicks(9764), "654321", "Jane", true, "Smith", 2, "$2a$11$6PJDl5SBTSvgPlVuiKicZu11l.EDGy/i7Kz9ywzZSdI.UGx3PEwn6", "Staff", new DateTime(2025, 8, 8, 17, 42, 13, 7, DateTimeKind.Utc).AddTicks(9773) }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedBy", "CompletedAt", "CreatedAt", "CurrentStatus", "Description", "DueDate", "IsArchived", "Priority", "TaskTitle", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8769), "Assigned", "Setup the initial project structure and repositories.", new DateTime(2025, 8, 18, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(7392), false, "High", "Initial Project Setup", new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8771) },
                    { 2, 1, null, new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8788), "InProgress", "Implement user login and registration functionality.", new DateTime(2025, 8, 28, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8781), false, "Medium", "Develop User Authentication", new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8788) }
                });

            migrationBuilder.InsertData(
                table: "TaskAssignments",
                columns: new[] { "Id", "AssignedPart", "AssignedTo", "CompletedAt", "CreatedAt", "DueDate", "Remarks", "Status", "UpdatedAt", "WorkTaskId" },
                values: new object[,]
                {
                    { 1, "Backend setup", 2, null, new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1143), new DateTime(2025, 8, 13, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(803), null, "InProgress", new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1144), 1 },
                    { 2, "Frontend implementation", 2, null, new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1154), new DateTime(2025, 8, 23, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1152), null, "Pending", new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1155), 2 }
                });

            migrationBuilder.InsertData(
                table: "TaskHistories",
                columns: new[] { "Id", "ChangeDescription", "ChangedByUserId", "CreatedAt", "NewStatus", "OldStatus", "UpdatedAt", "WorkTaskId" },
                values: new object[,]
                {
                    { 1, "Task created and assigned.", 1, new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3508), "Assigned", null, new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3518), 1 },
                    { 2, "User started working on the task.", 1, new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3986), "InProgress", "Assigned", new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3987), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OfficeName",
                table: "Offices",
                column: "OfficeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_AssignedTo",
                table: "TaskAssignments",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAssignments_WorkTaskId",
                table: "TaskAssignments",
                column: "WorkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_ChangedByUserId",
                table: "TaskHistories",
                column: "ChangedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistories_WorkTaskId",
                table: "TaskHistories",
                column: "WorkTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedBy",
                table: "Tasks",
                column: "AssignedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OfficeId",
                table: "Users",
                column: "OfficeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAssignments");

            migrationBuilder.DropTable(
                name: "TaskHistories");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
