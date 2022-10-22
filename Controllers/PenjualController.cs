using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Controllers;
public class PenjualController : Controller
{
    private readonly ILogger<PenjualController> _logger;
    private readonly AppDbContext _dbContext;
     public PenjualController(ILogger<PenjualController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //Untuk tambah data pembeli
    [HttpGet]
    public IActionResult CreatePenjual()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreatePenjual(Penjual br)
    {
        br.IdUser = 3;
        _dbContext.Penjual.Add(br);
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPenjual");
    }

    //untuk menampilkan data
     public IActionResult IndexPenjual()
    {
        List<Penjual> pembelis = _dbContext.Penjual.ToList();
        return View(pembelis);
    }

    //untuk update pembeli
    [HttpGet]
    public IActionResult UpdatePenjual(int id)
    {
        Penjual Id = _dbContext.Penjual.First(y => y.IdPenjual == id );
        return View(Id);
    }

    [HttpPost]
    public IActionResult UpdatePenjual(Penjual pm)
    {
        var penjualUpdate =_dbContext.Penjual.First(y => y.IdPenjual == pm.IdPenjual);
        penjualUpdate.Nama_Toko = pm.Nama_Toko;
        penjualUpdate.Alamat = pm.Alamat;
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPenjual");
    }
    //untuk delete pembeli
    [HttpGet]
    public IActionResult DeletePenjual(int id)
    {
        var penjualUpdate =_dbContext.Penjual.First(y => y.IdPenjual == id);
        _dbContext.Penjual.Remove(penjualUpdate);
        _dbContext.SaveChanges();
        return RedirectToAction("IndexPenjual");

    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}