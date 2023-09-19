using Microsoft.AspNetCore.Mvc;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Web.Controllers;

public class VillaNumberController(ApplicationDbContext dbContext) : Controller
{
    private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public IActionResult Index()
    {
        var villaNumbers = _dbContext.VillaNumbers;

        return View(villaNumbers);
    }

    public IActionResult Create()
    {
        return View();
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
