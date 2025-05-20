using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Data;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var noticias = await _context.Noticias.ToListAsync();
        return View(noticias);
    }
}
