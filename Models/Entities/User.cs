
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RAS.Bootcamp.mvc.Models.Entities;

public class User {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdUser{get; set;}
    [StringLength(10)]
    public string? Username{get; set;}
    [StringLength(20)]
    public string? Password{get; set;}
     [StringLength(20)]
    public int NoHandPhone{get; set;}
     [StringLength(20)]
    public string? tipe{get; set;}

} 