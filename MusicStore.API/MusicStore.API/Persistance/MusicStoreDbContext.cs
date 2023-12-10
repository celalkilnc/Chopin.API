using Microsoft.EntityFrameworkCore;
using MusicStore.API.Domain.Entities;

namespace MusicStore.API.Persistance;

public class MusicStoreDbContext : DbContext
{
    public MusicStoreDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Photo> Photos { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}   