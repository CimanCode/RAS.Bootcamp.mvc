namespace RAS.Bootcamp.mvc.Models;
public class ProductRequest {
    public int IdBarang{get; set;}
    public int Code {get; set;}
    public String? Nama {get;set;}
    public String? Description {get; set;}
    public int Harga {get; set;}
    public int stok {get; set;}
    public IFormFile? FileImage {get; set;}

    public String URL{get; set;}
}
