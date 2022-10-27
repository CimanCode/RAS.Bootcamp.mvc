using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;


namespace RAS.Bootcamp.mvc.Controllers;
[Authorize]
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly AppDbContext _dbContext;
    // public static List<Product> people = new List<Product>()
    //     {
    //            new Product { Code = 1, Name = "Laptop",Price = "2000000", Deskription = "Laptop for study", },
    //            new Product { Code = 2, Name = "Handphone", Price = "300000", Deskription = "handphone for playing game", },
    //     };

    public ProductController(ILogger<ProductController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //Untuk Nambah data
    [HttpGet]
    [Authorize(Roles = "PENJUAL, ADMIN")]
    public IActionResult index()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "PENJUAL, ADMIN")]
    public IActionResult index(ProductRequest newProduct)
    {
        var uploadImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");
        if(!Directory.Exists(uploadImage))
            Directory.CreateDirectory(uploadImage);
        
        var fileImage = $"{newProduct.Code}-{newProduct.FileImage.FileName}";
        var filePath = Path.Combine(uploadImage, fileImage);

        // Console.WriteLine(filePath);
        using var stream = System.IO.File.Create(filePath);
        if( newProduct.FileImage != null)
            newProduct.FileImage.CopyTo(stream);
        
        var Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Image/{fileImage}";
        _dbContext.Barang.Add(new Barang
        {
            IdPenjual = newProduct.IdBarang,
            Code = newProduct.Code,
            Nama = newProduct.Nama,
            Harga = newProduct.Harga,
            Description = newProduct.Description,
            stok = newProduct.stok,
            FileName = fileImage,
            URL = Url,
        });
        _dbContext.SaveChanges();
        List<Barang> data = _dbContext.Barang.ToList();
        return RedirectToAction("create", data);
    
    }
    
    //Untuk nampilin data
    [HttpGet]
    [Authorize(Roles = "PENJUAL")]
    public IActionResult create()
    {
        var dataid = int.Parse(User.Claims.First(x => x.Type == "ID").Value);
        var Penjual = _dbContext.Penjual.FirstOrDefault(y => y.IdUser == dataid);
        List<Barang> barangs = _dbContext.Barang.Where(x => x.IdPenjual == Penjual.IdUser).ToList();
        return View(barangs);
    } 

    //Untuk Update Data
    [HttpGet]
    [Authorize(Roles = "PENJUAL, ADMIN")]
    public IActionResult UpdateData(int id)
    {
        Barang Id = _dbContext.Barang.First(x => x.IdBarang == id);
        ProductRequest updateData = new ProductRequest
        {
            IdBarang = Id.IdBarang,
            Code = Id.Code,
            Nama = Id.Nama,
            Harga = Id.Harga,
            Description = Id.Description,
            stok = Id.stok,
        };
        return View(updateData);
    }

    
    [HttpPost]
    [Authorize(Roles = "PENJUAL, ADMIN")]
    public IActionResult UpdateData(ProductRequest newProduct)
    {
        var uploadImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image");
        var fileImage = $"{newProduct.Code}-{newProduct.FileImage.FileName}";
        var filePath = Path.Combine(uploadImage, fileImage);
        using var stream = System.IO.File.Create(filePath);
        if( newProduct.FileImage != null)
        {
            newProduct.FileImage.CopyTo(stream);
        }
        var Url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/Image/{fileImage}";
           
        Barang Id = _dbContext.Barang.First(x => x.IdBarang == newProduct.IdBarang);

        var DeleteBarang = Path.Combine(uploadImage, Id.FileName);
        System.IO.File.Delete(DeleteBarang);
        
        
        Id.Code = newProduct.Code;
        Id.Nama = newProduct.Nama;
        Id.Description = newProduct.Description;
        Id.Harga = newProduct.Harga;
        Id.stok = newProduct.stok;
        Id.FileName = fileImage;
        Id.URL = Url;

        _dbContext.SaveChanges();
        return RedirectToAction("create");

    }

    //Untuk Delete
    
    [HttpGet]
    [Authorize(Roles = "PENJUAL, ADMIN")]
    public IActionResult DeleteData(int id)
    {
        Barang barangdelete = _dbContext.Barang.First( x => x.IdBarang == id);
        var uploadImage = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Image");
        var filepath = Path.Combine(uploadImage, barangdelete.FileName);
        System.IO.File.Delete(filepath);

        _dbContext.Barang.Remove(barangdelete);
        _dbContext.SaveChanges();
        List<Barang> datas = _dbContext.Barang.ToList();
        return RedirectToAction("create", datas);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
