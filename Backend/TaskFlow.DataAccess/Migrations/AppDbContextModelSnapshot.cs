using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskFlow.DataAccess;

#nullable disable

namespace TaskFlow.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.7.24405.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskFlow.Core.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("OfficeName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OfficeName")
                        .IsUnique();

                    b.ToTable("Offices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 290, DateTimeKind.Utc).AddTicks(9987),
                            OfficeName = "Office1",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(401)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(746),
                            OfficeName = "Office2",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 291, DateTimeKind.Utc).AddTicks(747)
                        });
                });

            modelBuilder.Entity("TaskFlow.Core.Models.TaskAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AssignedPart")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("AssignedTo")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Remarks")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WorkTaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AssignedTo");

                    b.HasIndex("WorkTaskId");

                    b.ToTable("TaskAssignments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedPart = "Backend setup",
                            AssignedTo = 2,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1143),
                            DueDate = new DateTime(2025, 8, 13, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(803),
                            Status = "InProgress",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1144),
                            WorkTaskId = 1
                        },
                        new
                        {
                            Id = 2,
                            AssignedPart = "Frontend implementation",
                            AssignedTo = 2,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1154),
                            DueDate = new DateTime(2025, 8, 23, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1152),
                            Status = "Pending",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(1155),
                            WorkTaskId = 2
                        });
                });

            modelBuilder.Entity("TaskFlow.Core.Models.TaskHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ChangeDescription")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<int>("ChangedByUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NewStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OldStatus")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("WorkTaskId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChangedByUserId");

                    b.HasIndex("WorkTaskId");

                    b.ToTable("TaskHistories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChangeDescription = "Task created and assigned.",
                            ChangedByUserId = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3508),
                            NewStatus = "Assigned",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3518),
                            WorkTaskId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChangeDescription = "User started working on the task.",
                            ChangedByUserId = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3986),
                            NewStatus = "InProgress",
                            OldStatus = "Assigned",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 9, DateTimeKind.Utc).AddTicks(3987),
                            WorkTaskId = 2
                        });
                });

            modelBuilder.Entity("TaskFlow.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("OfficeId")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 693, DateTimeKind.Utc).AddTicks(7355),
                            EmployeeID = "123456",
                            FirstName = "John",
                            IsActive = true,
                            LastName = "Doe",
                            OfficeId = 1,
                            PasswordHash = "$2a$11$eLmtnQ7l3hm2ROz6UzcfLOE9JZnyWzCduGPvqLVL9ITQfTzvRyCXe",
                            Title = "DepartmentHead",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 12, 693, DateTimeKind.Utc).AddTicks(7366)
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 7, DateTimeKind.Utc).AddTicks(9764),
                            EmployeeID = "654321",
                            FirstName = "Jane",
                            IsActive = true,
                            LastName = "Smith",
                            OfficeId = 2,
                            PasswordHash = "$2a$11$6PJDl5SBTSvgPlVuiKicZu11l.EDGy/i7Kz9ywzZSdI.UGx3PEwn6",
                            Title = "Staff",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 7, DateTimeKind.Utc).AddTicks(9773)
                        });
                });

            modelBuilder.Entity("TaskFlow.Core.Models.WorkTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaskTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AssignedBy");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedBy = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8769),
                            CurrentStatus = "Assigned",
                            Description = "Setup the initial project structure and repositories.",
                            DueDate = new DateTime(2025, 8, 18, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(7392),
                            IsArchived = false,
                            Priority = "High",
                            TaskTitle = "Initial Project Setup",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8771)
                        },
                        new
                        {
                            Id = 2,
                            AssignedBy = 1,
                            CreatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8788),
                            CurrentStatus = "InProgress",
                            Description = "Implement user login and registration functionality.",
                            DueDate = new DateTime(2025, 8, 28, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8781),
                            IsArchived = false,
                            Priority = "Medium",
                            TaskTitle = "Develop User Authentication",
                            UpdatedAt = new DateTime(2025, 8, 8, 17, 42, 13, 8, DateTimeKind.Utc).AddTicks(8788)
                        });
                });

            modelBuilder.Entity("TaskFlow.Core.Models.TaskAssignment", b =>
                {
                    b.HasOne("TaskFlow.Core.Models.User", "AssignedToUser")
                        .WithMany()
                        .HasForeignKey("AssignedTo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TaskFlow.Core.Models.WorkTask", "WorkTask")
                        .WithMany("Assignments")
                        .HasForeignKey("WorkTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedToUser");

                    b.Navigation("WorkTask");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.TaskHistory", b =>
                {
                    b.HasOne("TaskFlow.Core.Models.User", "ChangedByUser")
                        .WithMany("TaskHistoryEntries")
                        .HasForeignKey("ChangedByUserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("TaskFlow.Core.Models.WorkTask", "WorkTask")
                        .WithMany("History")
                        .HasForeignKey("WorkTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChangedByUser");

                    b.Navigation("WorkTask");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.User", b =>
                {
                    b.HasOne("TaskFlow.Core.Models.Office", "Office")
                        .WithMany("Users")
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Office");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.WorkTask", b =>
                {
                    b.HasOne("TaskFlow.Core.Models.User", "AssignedByUser")
                        .WithMany()
                        .HasForeignKey("AssignedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssignedByUser");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.Office", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.User", b =>
                {
                    b.Navigation("TaskHistoryEntries");
                });

            modelBuilder.Entity("TaskFlow.Core.Models.WorkTask", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("History");
                });
#pragma warning restore 612, 618
        }
    }
}