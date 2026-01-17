using Microsoft.EntityFrameworkCore;
using PoliNote.Models;

namespace PoliNote.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // role
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

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
            }
        );
    }
}
