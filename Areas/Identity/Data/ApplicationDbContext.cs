using IsTakipSistemi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IsTakipSistemi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Timeline> Timelines { get; set; }
        public DbSet<TimelineEvent> TimelineEvents { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

                // User-Role relationship
                builder.Entity<UserRole>()
                   .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // Task-User relationship
            builder.Entity<Models.Task>()
                .HasOne(t => t.AssignedUser)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Task-Project relationship
            builder.Entity<Models.Task>()
                .HasOne(t => t.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(t => t.ProjectId);

            // Timeline-TimelineEvent relationship
            builder.Entity<TimelineEvent>()
                .HasOne(e => e.Timeline)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.TimelineId);

            // CalendarEvent-User relationship
            builder.Entity<CalendarEvent>()
                .HasOne(e => e.User)
                .WithMany(u => u.CalendarEvents)
                .HasForeignKey(e => e.UserId);  
        }
        public DbSet<IsTakipSistemi.Models.UserRole> UserRole { get; set; } = default!;
    }
}
