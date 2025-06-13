using Microsoft.EntityFrameworkCore;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0;

public class AppDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<SchoolEntitiy> School { get; set; } = null!;
    public DbSet<ClassEntity> Class { get; set; } = null!;

    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserEntity>()
            .OwnsOne(e=>e.)
    }
    
    
    */
}