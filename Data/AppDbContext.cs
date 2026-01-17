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

        // Dummy data
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", FirstName = "Jan", LastName="Kowalski", PasswordHash= "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", Role="admin"  },
            new User { Id = 2, Username = "testuser", PasswordHash = "$2a$12$tDPBut7pAwtiZMA.Wq1IqOhpq0jGxGcTrbdIlXIrjhv7uJX4bcHka", Role ="user" }
        );
    }
}
