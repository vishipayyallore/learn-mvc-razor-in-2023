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

    [HttpPost]
    public IActionResult Create(VillaNumberVM villaNumberVM)
    {
        // Method 1
        // ModelState.Remove("Villa");

        bool roomNumberExists = _dbContext.VillaNumbers.Any(r => r.Villa_Number == villaNumberVM.VillaNumber!.Villa_Number);

        if (ModelState.IsValid && !roomNumberExists)
        {
            _dbContext.VillaNumbers.Add(villaNumberVM.VillaNumber!);
            _dbContext.SaveChanges();

            TempData["success"] = "The Villa Number has been created successfully.";

            return RedirectToAction(nameof(Index));
        }

        if (roomNumberExists)
        {
            TempData["error"] = "The Villa Number already exists.";
        }

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

}
