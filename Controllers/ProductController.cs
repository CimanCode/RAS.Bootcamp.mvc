using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Controllers;
public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly AppDbContext _dbContext;
    public static List<Product> people = new List<Product>()
        {
               new Product { Code = 1, Name = "Laptop",Price = "2000000", Deskription = "Laptop for study", },
               new Product { Code = 2, Name = "Handphone", Price = "300000", Deskription = "handphone for playing game", },
        };

    public ProductController(ILogger<ProductController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //Untuk Nambah data
    [HttpGet]
    public IActionResult index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult index(Barang per)
    {
        per.IdPenjual= 6;
        _dbContext.Barang.Add(per);
        _dbContext.SaveChanges();
        return RedirectToAction("create");
    
    }

    //Untuk nampilin data
    public IActionResult create()
    {
        List<Barang> barangs = _dbContext.Barang.ToList();
        return View(barangs);
    } 

    //Untuk Update Data
    [HttpGet]
    public IActionResult UpdateData(int id)
    {
        Barang Id = _dbContext.Barang.First(x => x.IdBarang == id);
        return View(Id);
    }

    [HttpPost]
    public IActionResult UpdateData(Barang per)
    {
        var barangUpdate = _dbContext.Barang.First( x => x.IdBarang == per.IdBarang);
        barangUpdate.Code = per.Code;
        barangUpdate.Nama = per.Nama;
        barangUpdate.Description = per.Description;
        barangUpdate.Harga = per.Harga;
        barangUpdate.stok = per.stok;
        _dbContext.SaveChanges();
        return RedirectToAction("create");

    }

    //Untuk Delete
    [HttpGet]
    public IActionResult DeleteData(int id)
    {
        var barangUpdate = _dbContext.Barang.First( x => x.IdBarang == id);
        _dbContext.Barang.Remove(barangUpdate);
        _dbContext.SaveChanges();
        return RedirectToAction("create");
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
