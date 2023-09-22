using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;
using Resorts.Web.ViewModels;

namespace Resorts.Web.Controllers;

public class VillaNumberController(ApplicationDbContext dbContext) : Controller
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public IActionResult Index()
    {
        var villaNumbers = _dbContext.VillaNumbers.Include(r => r.Villa).ToList();

        return View(villaNumbers);
    }

    public IActionResult Create()
    {
        //IEnumerable<SelectListItem> villaList = GetVillaList();

        // ViewData is Dictionary
        // ViewData["VillaList"] = villaList;

        // ViewBag is Dynamic Type
        //ViewBag.VillaList = villaList;

        VillaNumberVM villaNumberVM = new()
        {
            VillaList = GetVillaList()
        };

        return View(villaNumberVM);
    }

    private IEnumerable<SelectListItem> GetVillaList()
    {
        return _dbContext.Villas.Select(r => new SelectListItem
        {
            Text = r.Name,
            Value = $"{r.Id}",
        });
    }

    [HttpPost]
    public IActionResult Create(VillaNumber villaNumber)
    {
        // Method 1
        // ModelState.Remove("Villa");

        if (ModelState.IsValid)
        {
            _dbContext.VillaNumbers.Add(villaNumber);
            _dbContext.SaveChanges();

            TempData["success"] = "The Villa Number has been created successfully.";

            return RedirectToAction(nameof(Index));
        }

        return View(villaNumber);
    }

}
