using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelPlaces.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelPlaces.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly Ace42023Context db;
    public HomeController(ILogger<HomeController> logger, Ace42023Context _db)
    {
        _logger = logger;
        db = _db;
    }

    [HttpGet]
    public IActionResult Index(string search)
    {
        List<TouristPlace> T = db.TouristPlaces.Include(P => P.PlaceImages).ToList();
        if (!string.IsNullOrEmpty(search))
        {
            T = T.Where(x => x.Name.Contains(search, StringComparison.OrdinalIgnoreCase) || x.City.Contains(search, StringComparison.OrdinalIgnoreCase) || x.State.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return View(T);
    }
    [HttpGet]
    public IActionResult Place(int id)
    {
        var T = db.TouristPlaces.Include(p => p.Providers).Include(p => p.PlaceImages).FirstOrDefault(p => p.PlaceId == id);
        return View(T);
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
