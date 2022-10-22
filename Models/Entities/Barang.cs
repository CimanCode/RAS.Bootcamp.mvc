
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RAS.Bootcamp.mvc.Models.Entities;

public class Barang {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBarang{get; set;}
    public int Code{get; set;}
    [StringLength(10)]
    public string? Nama{get; set;}
    public string? Description{get; set;}
    public int Harga{get; set;}
    public int stok{get; set;}
    [ForeignKey("Penjual")]
    public int IdPenjual{get; set;}
    [StringLength(250)]
    public string FileName{get; set;}
    [StringLength(250)]
    public string URL{get; set;}

    public virtual Penjual? Penjual { get; set; }

} 