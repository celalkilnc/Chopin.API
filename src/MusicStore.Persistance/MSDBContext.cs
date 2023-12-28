using Microsoft.EntityFrameworkCore;
using MusicStore.Domain.Entities;

namespace MusicStore.Persistance;

public class MSDBContext : DbContext
{
    public MSDBContext(DbContextOptions options) : base(options) { }
    
    public DbSet<Photo> Photos { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }
}