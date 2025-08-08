using Microsoft.EntityFrameworkCore;
using TaskFlow.Core.Models;
using TaskFlow.Core.Models.Enums;
using BCrypt.Net;

namespace TaskFlow.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<TaskHistory> TaskHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Office Seed Data
            modelBuilder.Entity<Office>().HasData(
                new Office { Id = 1, OfficeName = OfficeNameType.Office1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Office { Id = 2, OfficeName = OfficeNameType.Office2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            // User Seed Data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    EmployeeID = "123456",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Title = TitleType.DepartmentHead,
                    OfficeId = 1,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    EmployeeID = "654321",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password456"),
                    Title = TitleType.Staff,
                    OfficeId = 2,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // WorkTask Seed Data
            modelBuilder.Entity<WorkTask>().HasData(
                new WorkTask
                {
                    Id = 1,
                    TaskTitle = "Initial Project Setup",
                    Description = "Setup the initial project structure and repositories.",
                    AssignedBy = 1,
                    DueDate = DateTime.UtcNow.AddDays(10),
                    Priority = PriorityType.High,
                    CurrentStatus = TaskStatusType.Assigned,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new WorkTask
                {
                    Id = 2,
                    TaskTitle = "Develop User Authentication",
                    Description = "Implement user login and registration functionality.",
                    AssignedBy = 1,
                    DueDate = DateTime.UtcNow.AddDays(20),
                    Priority = PriorityType.Medium,
                    CurrentStatus = TaskStatusType.InProgress,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // TaskAssignment Seed Data
            modelBuilder.Entity<TaskAssignment>().HasData(
                new TaskAssignment
                {
                    Id = 1,
                    WorkTaskId = 1,
                    AssignedTo = 2,
                    AssignedPart = "Backend setup",
                    Status = AssignmentStatusType.InProgress,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new TaskAssignment
                {
                    Id = 2,
                    WorkTaskId = 2,
                    AssignedTo = 2,
                    AssignedPart = "Frontend implementation",
                    Status = AssignmentStatusType.Pending,
                    DueDate = DateTime.UtcNow.AddDays(15),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            // TaskHistory Seed Data
            modelBuilder.Entity<TaskHistory>().HasData(
                new TaskHistory
                {
                    Id = 1,
                    WorkTaskId = 1,
                    ChangedByUserId = 1,
                    OldStatus = null,
                    NewStatus = TaskStatusType.Assigned,
                    ChangeDescription = "Task created and assigned.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new TaskHistory
                {
                    Id = 2,
                    WorkTaskId = 2,
                    ChangedByUserId = 1,
                    OldStatus = TaskStatusType.Assigned,
                    NewStatus = TaskStatusType.InProgress,
                    ChangeDescription = "User started working on the task.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).ValueGeneratedOnAdd();
                
                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.EmployeeID)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(u => u.Title)
                    .HasConversion<string>();

                entity.HasOne(u => u.Office)
                    .WithMany(o => o.Users)
                    .HasForeignKey(u => u.OfficeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WorkTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();
                
                entity.Property(t => t.TaskTitle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(t => t.Description)
                    .HasMaxLength(1000);

                entity.Property(t => t.AssignedBy)
                    .IsRequired();

                entity.HasOne(t => t.AssignedByUser)
                    .WithMany()
                    .HasForeignKey(t => t.AssignedBy)
                    .OnDelete(DeleteBehavior.Restrict);
                
                entity.Property(t => t.Priority)
                    .IsRequired()
                    .HasConversion<string>();
                
                entity.Property(t => t.CurrentStatus)
                    .IsRequired()
                    .HasConversion<string>();
            });
            
            modelBuilder.Entity<TaskAssignment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Id).ValueGeneratedOnAdd();
                
                entity.Property(a => a.WorkTaskId)
                    .IsRequired();
                    
                entity.Property(a => a.AssignedTo)
                    .IsRequired();
                
                entity.HasOne(a => a.WorkTask)
                    .WithMany(t => t.Assignments)
                    .HasForeignKey(a => a.WorkTaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.AssignedToUser)
                    .WithMany()
                    .HasForeignKey(a => a.AssignedTo)
                    .OnDelete(DeleteBehavior.NoAction);
                
                entity.Property(a => a.AssignedPart)
                    .HasMaxLength(255);

                entity.Property(a => a.Status)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(a => a.Remarks)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<TaskHistory>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Id).ValueGeneratedOnAdd();
                
                entity.Property(h => h.WorkTaskId)
                    .IsRequired();

                entity.Property(h => h.ChangedByUserId)
                    .IsRequired();
                    
                entity.HasOne(h => h.WorkTask)
                    .WithMany(t => t.History)
                    .HasForeignKey(h => h.WorkTaskId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(h => h.ChangedByUser)
                    .WithMany(u => u.TaskHistoryEntries)
                    .HasForeignKey(h => h.ChangedByUserId)
                    .OnDelete(DeleteBehavior.SetNull);
                
                entity.Property(h => h.NewStatus)
                    .IsRequired()
                    .HasConversion<string>();

                entity.Property(h => h.OldStatus)
                    .HasConversion<string>();
                
                entity.Property(h => h.ChangeDescription)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).ValueGeneratedOnAdd();
                
                entity.Property(o => o.OfficeName)
                    .IsRequired()
                    .HasConversion<string>();
                
                entity.HasIndex(o => o.OfficeName).IsUnique();
            });
        }
    }
}