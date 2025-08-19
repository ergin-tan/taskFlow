using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TaskFlow.Core.Models;
using TaskFlow.Core.Models.Enums;
using BCrypt.Net;
using System;

namespace TaskFlow.DataAccess
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<WorkTask> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Metadata.GetTableName(),
                    UserId = !string.IsNullOrEmpty(userId) ? int.Parse(userId) : null
                };
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            break;
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }

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

    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }
        public EntityEntry Entry { get; }
        public int? UserId { get; set; }
        public AuditType AuditType { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();
        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditLog ToAudit()
        {
            var audit = new AuditLog
            {
                UserId = UserId,
                TableName = TableName,
                Timestamp = DateTime.UtcNow,
                PrimaryKey = JsonSerializer.Serialize(KeyValues),
                Action = AuditType.ToString(),
                OldValues = OldValues.Count == 0 ? "{}" : JsonSerializer.Serialize(OldValues),
                NewValues = NewValues.Count == 0 ? "{}" : JsonSerializer.Serialize(NewValues)
            };
            return audit;
        }
    }

    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}