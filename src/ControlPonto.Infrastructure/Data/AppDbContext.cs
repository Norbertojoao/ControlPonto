using ControlPonto.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlPonto.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<TimeRecord> TimeRecords => Set<TimeRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            entity.Property(e => e.RegistrationCode).IsRequired().HasMaxLength(30);
            entity.HasIndex(e => e.RegistrationCode).IsUnique();
            entity.Property(e => e.Department).IsRequired().HasMaxLength(80);
        });

        modelBuilder.Entity<TimeRecord>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Notes).HasMaxLength(250);
            entity.HasOne(t => t.Employee)
                  .WithMany(e => e.TimeRecords)
                  .HasForeignKey(t => t.EmployeeId);
        });
    }
}
