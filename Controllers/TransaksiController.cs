using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using RAS.Bootcamp.mvc.Models;
using Microsoft.EntityFrameworkCore;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.Catalog.Mvc.Net.Controllers;

public class TransaksiController : Controller
{
    private readonly ILogger<TransaksiController> _logger;
    private readonly AppDbContext _dbContext;

    public TransaksiController(ILogger<TransaksiController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult DataTransaksi()
    {
       if(User.IsInRole("PENJUAL"))
       {
        int datauser = int.Parse(User.Claims.First(x => x.Type == "ID").Value);
        Penjual pj = _dbContext.Penjual.First(x => x.IdUser == datauser);
        var transaksi = (from a in _dbContext.Transaksis
                         join b in _dbContext.ItemTransaksis on a.IdTransaksi equals b.IdTransaksi
                         join c in _dbContext.Barang on b.IdBarang equals c.IdBarang
                         where c.IdPenjual == pj.IdUser
                         group a by a.IdTransaksi into Transaksi
                         select Transaksi.FirstOrDefault()).ToList();
        return View(transaksi);
       }
       List<Transaksi> ltr = _dbContext.Transaksis.ToList();
       return View(ltr);
    }

    public IActionResult DetailTransaksi(int id)
    {
        var transaksi = _dbContext.Transaksis.First(x => x.IdTransaksi == id);
        var pb = _dbContext.Pembelies.First(x => x.IdUser == transaksi.IdUser);
        return View(transaksi);
    }

    public IActionResult KonfirTransaksi(int id)
    {
        var transaksi = _dbContext.Transaksis.First(x => x.IdTransaksi == id);
        transaksi.StatusTransaksi = "Sedang di Proses";
        transaksi.StatusBayar = "Telah di Bayar";
        _dbContext.SaveChanges();
        List<Transaksi> Ltr = _dbContext.Transaksis.ToList();
        return View("DataTransaksi", Ltr);
    }

    public IActionResult BatalTransaksi(int id)
    {
         var transaksi = _dbContext.Transaksis.First(x => x.IdTransaksi == id);
        transaksi.StatusTransaksi = "Di Batalkan";
        transaksi.StatusBayar = "Di Batalkan";
        _dbContext.SaveChanges();
        int datauser = int.Parse(User.Claims.First(x => x.Type == "ID").Value);
        Penjual pj = _dbContext.Penjual.First(x => x.IdUser == datauser);
        var btr = (from a in _dbContext.Transaksis
                         join b in _dbContext.ItemTransaksis on a.IdTransaksi equals b.IdTransaksi
                         join c in _dbContext.Barang on b.IdBarang equals c.IdBarang
                         where c.IdPenjual == pj.IdUser
                         group a by a.IdTransaksi into Transaksi
                         select Transaksi.FirstOrDefault()).ToList();
        return View("DataTransaksi", btr);
    }

    public IActionResult KirimBarang(int id)
    {
        var transaksi = _dbContext.Transaksis.First(x => x.IdTransaksi == id);
        transaksi.StatusTransaksi = "Dikirimkan";
        _dbContext.SaveChanges();
        int datauser = int.Parse(User.Claims.First(x => x.Type == "ID").Value);
        Penjual pj = _dbContext.Penjual.First(x => x.IdUser == datauser);
        var ktr = (from a in _dbContext.Transaksis
                         join b in _dbContext.ItemTransaksis on a.IdTransaksi equals b.IdTransaksi
                         join c in _dbContext.Barang on b.IdBarang equals c.IdBarang
                         where c.IdPenjual == pj.IdUser
                         group a by a.IdTransaksi into Transaksi
                         select Transaksi.FirstOrDefault()).ToList();
        return View("DataTransaksi", ktr);
    }
}