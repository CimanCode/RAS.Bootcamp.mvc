using Microsoft.EntityFrameworkCore;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Models;

public class AppDbContext: DbContext {
    public AppDbContext (DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    //untuk menyimpan data user
    public DbSet<User> User { get; set; }
    //untuk menyimpan data penjual
    public DbSet<Penjual> Penjual { get; set; }
    //untuk menyimpan data pemebli
    public DbSet<Pembeli> Pembelies { get; set; }
    //untuk menyimpan data barang
    public DbSet<Barang> Barang { get; set; }
    public DbSet<Transaksi> Transaksis {get; set;}
    public DbSet<ItemTransaksi> ItemTransaksis {get; set;}
    public DbSet<Keranjang> Keranjangs {get; set;}
}