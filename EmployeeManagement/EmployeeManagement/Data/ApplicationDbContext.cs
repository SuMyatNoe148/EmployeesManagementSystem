using EmployeeManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<SystemCode> SystemCodes { get; set; }
        public DbSet<SystemCodeDetail> systemCodeDetails { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<LeaveApplication> leaveApplications { get; set; }
        public DbSet<SystemProfile> systemProfiles { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<RoleProfile> RoleProfiles { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<LeaveAdjustmentEntry> LeaveAdjustmentEntries { get; set; }
        public DbSet<LeavePeriod> LeavePeriods { get; set; }
        public DbSet<CompanyInformation> CompanyInformations { get; set; }
        public DbSet<ApprovalEntry> ApprovalEntries { get; set; }
        public DbSet<WorkFlowUserGroup> WorkFlowUserGroups { get; set; }
        public DbSet<WorkFlowUserGroupMember> WorkFlowUserGroupMembers { get; set; }
        public DbSet<ApprovalsUserMatrix> ApprovalsUserMatrixs { get; set; }
        public virtual async Task<int> SaveChangesAsync(string userId = null)
        {
            OnBeforeSavingChanges(userId);
            var result = await base.SaveChangesAsync();
            return result;
        }

        private void OnBeforeSavingChanges(string userId)
        {
            ChangeTracker.DetectChanges();

            var auditEntries = new List<AuditEntry>();
            var entries = ChangeTracker.Entries().ToList(); // ✅ FIX

            foreach (var entry in entries)
            {
                if (entry.Entity is Audit ||
                    entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                var user = _httpContextAccessor.HttpContext?.User;
                var userIdValue = userId
                    ?? user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                    ?? "System";

                auditEntry.UserId = userIdValue;

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                    }
                    else
                    {
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
                                    auditEntry.ChangedColumns.Add(propertyName);
                                    auditEntry.AuditType = AuditType.Update;
                                    auditEntry.OldValues[propertyName] = property.OriginalValue;
                                    auditEntry.NewValues[propertyName] = property.CurrentValue;
                                }
                                break;
                        }
                    }
                }
            }

            // ✅ ADD AUDITS AFTER LOOP
            foreach (var auditEntry in auditEntries)
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // LeaveApplication
            modelBuilder.Entity<LeaveApplication>()
                .HasOne(x => x.Duration)
                .WithMany()
                .HasForeignKey(x => x.DurationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApprovalEntry
            modelBuilder.Entity<ApprovalEntry>()
                .HasOne(a => a.Approver)
                .WithMany()
                .HasForeignKey(a => a.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApprovalEntry>()
                .HasOne(a => a.LastModifiedBy)
                .WithMany()
                .HasForeignKey(a => a.LastModifiedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ ADD THESE TWO
            modelBuilder.Entity<ApprovalEntry>()
                .HasOne(a => a.DocumentType)
                .WithMany()
                .HasForeignKey(a => a.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApprovalEntry>()
                .HasOne(a => a.Status)
                .WithMany()
                .HasForeignKey(a => a.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowUserGroupMember>()
               .HasOne(w => w.Sender)
               .WithMany()
               .HasForeignKey(w => w.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkFlowUserGroupMember>()
                .HasOne(w => w.Approver)
                .WithMany()
                .HasForeignKey(w => w.ApproverId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkFlowUserGroupMember>()
                .HasOne(w => w.WorkFlowUserGroup)
                .WithMany()
                .HasForeignKey(w => w.WorkFlowUserGroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
