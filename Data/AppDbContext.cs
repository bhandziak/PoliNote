using Microsoft.EntityFrameworkCore;
using PoliNote.Models.Calendar;
using PoliNote.Models.Notes;
using PoliNote.Models.Subjects;
using PoliNote.Models.Users;

namespace PoliNote.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<PublicEvent> PublicEvents => Set<PublicEvent>();
    public DbSet<PrivateEvent> PrivateEvents => Set<PrivateEvent>();
    // subjects
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<SubjectGroup> SubjectGroups => Set<SubjectGroup>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    // notes
    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // roles
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        // --- RELATIONS ---

        // Users -> PublicEvents
        modelBuilder.Entity<PublicEvent>()
            .HasOne(e => e.CreatedByUser)
            .WithMany(u => u.PublicEvents)
            .HasForeignKey(e => e.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Users -> PrivateEvents
        modelBuilder.Entity<PrivateEvent>()
            .HasOne(e => e.CreatedByUser)
            .WithMany(u => u.PrivateEvents)
            .HasForeignKey(e => e.CreatedByUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // --- RELATIONS FOR SUBJECTS ---

        // Subject -> SubjectGroups (One-to-Many)
        modelBuilder.Entity<SubjectGroup>()
            .HasOne(sg => sg.Subject)
            .WithMany(s => s.Groups)
            .HasForeignKey(sg => sg.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // SubjectGroup -> Enrollments (One-to-Many)
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.SubjectGroup)
            .WithMany(sg => sg.Enrollments)
            .HasForeignKey(e => e.SubjectGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Enrollments (One-to-Many)
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.User)
            .WithMany(u => u.Enrollments)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Enroll only once
        modelBuilder.Entity<Enrollment>()
            .HasIndex(e => new { e.UserId, e.SubjectGroupId })
            .IsUnique();

        // ---  NOTES ---
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(n => n.TargetDate)
                .HasColumnType("date")
                .IsRequired();

            // User -> Note
            entity.HasOne(n => n.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // SubjectGroup -> Note
            entity.HasOne(n => n.SubjectGroup)
                .WithMany(sg => sg.Notes)
                .HasForeignKey(n => n.SubjectGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(n => new { n.UserId, n.SubjectGroupId, n.TargetDate })
                .IsUnique();

            entity.Property(n => n.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        // Dummy data
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Username = "admin",
                Email = "admin@example.com",
                FirstName = "Jan",
                LastName = "Kowalski",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Admin,
                IsActivated = true
            },
            new User
            {
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Student,
                IsActivated =true
            },
            new User
            {
                Username = "informator1",
                Email = "informator1@example.com",
                FirstName = "Marek",
                LastName = "Nowak",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Informant,
                IsActivated = true
            },
            new User
            {
                Username = "informator2",
                Email = "informator2@example.com",
                FirstName = "Anna",
                LastName = "Wiśniewska",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Informant,
                IsActivated = false
            }
        );
    }
}
