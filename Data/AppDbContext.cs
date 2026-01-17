using Microsoft.EntityFrameworkCore;
using PoliNote.Models;

namespace PoliNote.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<PublicEvent> PublicEvents => Set<PublicEvent>();

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

        // Dummy data
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                FirstName = "Jan",
                LastName = "Kowalski",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Admin
            },
            new User
            {
                Id = 2,
                Username = "testuser",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Student
            },
            new User
            {
                Id = 3,
                Username = "informator1",
                FirstName = "Marek",
                LastName = "Nowak",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Informant
            },
            new User
            {
                Id = 4,
                Username = "informator2",
                FirstName = "Anna",
                LastName = "Wiśniewska",
                PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka",
                Role = UserRole.Informant
            }
        );
    }
}
