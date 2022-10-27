using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RAS.Bootcamp.mvc.Models.Entities;

public class ItemTransaksi
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get; set;}
    [ForeignKey("Barang")]
    public int IdBarang {get; set;}
    public decimal Harga {get; set;}
    public int Jumlah{get; set;}
    public decimal SubTotal{get; set;}
    [ForeignKey("Transaksi")]
    public int IdTransaksi{get; set;}

    public virtual Barang Barang{get; set;}
    public virtual Transaksi Transaksi{get; set;}
}