
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RAS.Bootcamp.mvc.Models.Entities;

public class Penjual {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPenjual{get; set;}
    [ForeignKey("User")]
    public int IdUser{get; set;}
    [StringLength(20)]
    public string? Nama_Toko{get; set;}
    [StringLength(20)]
    public string? Alamat{get; set;}

    public virtual User? User { get; set;}
    public virtual ICollection<Barang>? Barang { get; set; }

} 