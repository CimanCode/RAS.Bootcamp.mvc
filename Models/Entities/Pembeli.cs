
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RAS.Bootcamp.mvc.Models.Entities;

public class Pembeli {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPembeli{get; set;}
    [ForeignKey("User")]
    public int IdUser{get; set;}
    [StringLength(20)]
    public string? Nama{get; set;}
    public string? No_HandPhone{get; set;}
    [StringLength(12)]
    public string? Alamat{get; set;}

    public virtual User? User { get; set;}
} 