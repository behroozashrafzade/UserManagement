using Microsoft.EntityFrameworkCore;
using WebApplication._9._0.Entities;

namespace WebApplication._9._0;

public class AppDbContext(DbContextOptions options): DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<SchoolEntitiy> School { get; set; }
    public DbSet<ClassEntity> Class { get; set; }
    
}