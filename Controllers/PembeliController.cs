using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Controllers;
public class PembeliController : Controller
{
    private readonly ILogger<PembeliController> _logger;
    private readonly AppDbContext _dbContext;
     public PembeliController(ILogger<PembeliController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //Untuk tambah data pembeli
    [HttpGet]
    public IActionResult CreatePembeli()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreatePembeli(Pembeli br)
    {
        br.IdUser = 1;
        _dbContext.Pembelies.Add(br);
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPembeli");   
    }

    //untuk menampilkan data
     public IActionResult IndexPembeli()
    {
        List<Pembeli> pembelis = _dbContext.Pembelies.ToList();
        return View(pembelis); 
    }

    //untuk update pembeli
    [HttpGet]
    public IActionResult UpdatePembeli(int id)
    {
        Pembeli Id = _dbContext.Pembelies.First(y => y.IdPembeli == id );
        return View(Id);
    }

    [HttpPost]
    public IActionResult UpdatePembeli(Pembeli pm)
    {
        var pembeliUpdate =_dbContext.Pembelies.First(y => y.IdPembeli == pm.IdPembeli);
        pembeliUpdate.Nama = pm.Nama;
        pembeliUpdate.No_HandPhone = pm.No_HandPhone;
        pembeliUpdate.Alamat = pm.Alamat;
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPembeli");
    }
    //untuk delete pembeli
    [HttpGet]
    public IActionResult DeletePembeli(int id)
    {
        var pembeliUpdate =_dbContext.Pembelies.First(y => y.IdPembeli == id);
        _dbContext.Pembelies.Remove(pembeliUpdate);
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPembeli");

    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}