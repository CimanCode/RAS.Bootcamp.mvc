using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;
using System.Collections.Generic;

namespace RAS.Bootcamp.mvc.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext  _dbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Barang> barangs = _dbContext.Barang.ToList();
        return View(barangs);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Forbidden()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
