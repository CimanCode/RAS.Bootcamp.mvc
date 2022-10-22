using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RAS.Bootcamp.mvc.Models;
using RAS.Bootcamp.mvc.Models.Entities;

namespace RAS.Bootcamp.mvc.Controllers;
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly AppDbContext _dbContext;
     public AccountController(ILogger<AccountController> logger, AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    //Untuk tambah data pembeli
    [HttpGet]
    public IActionResult Login()
    {
        // List<User> = _dbContext.User.ToList();
        return View(new LoginRequest());
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest br)
    {
        if(!ModelState.IsValid)
        {
        return View(br);
        }

        var user = _dbContext.User.FirstOrDefault(X => X.Username == br.Username && X.Password == br.Password);

        if(user == null)
        {
            ViewBag.ErrorMessage = "password atawa usernamena salah bro";
            return View(br);
        }

        // if(user.tipe == "PEMBELI")
        // {
        //     ViewBag.ErrorMessage = "Maneh lain admin jeung lain tukang dagang";
        //     return View(br);
        // }

        //set autorization data to cookies
        var claims = new List<Claim>{
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Fullname", user.Username),
            new Claim(ClaimTypes.Role, user.tipe),
        };

        var calimsIdentity = new ClaimsIdentity
        (claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20),
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(calimsIdentity),
            authProperties);
        
        return RedirectToAction("Index", "Home");
    }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login");
        }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Register(RegisterRequest request)
    {
        if(!ModelState.IsValid)
        {
            return View(request);
        }

        var newUser = new Models.Entities.User
        {
            Username = request.Username,
            Password = request.Password,
            tipe = request.tipe,
        };

        var Penjual = new Models.Entities.Penjual
        {
            IdUser = newUser.IdUser,
            Alamat = request.Alamat,
            Nama_Toko = $"TK {request.FullName}",
            User = newUser
        };

        _dbContext.User.Add(newUser);
        _dbContext.Penjual.Add(Penjual);

        _dbContext.SaveChanges();

        return RedirectToAction("Login");
    }

}