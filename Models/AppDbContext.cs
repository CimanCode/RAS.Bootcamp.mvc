using Microsoft.EntityFrameworkCore;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Models;

public class AppDbContext: DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    public DbSet<User> User { get; set; }
    public DbSet<Penjual> Penjual { get; set; }
    public DbSet<Pembeli> Pembelies { get; set; }
    public DbSet<Barang> Barang { get; set; }

}