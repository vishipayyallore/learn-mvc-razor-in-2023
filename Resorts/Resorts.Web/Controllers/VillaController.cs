using Microsoft.AspNetCore.Mvc;
using Resorts.Domain.Entities;
using Resorts.Infrastructure.Data;

namespace Resorts.Web.Controllers;

public class VillaController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public VillaController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IActionResult Index()
    {
        var villas = _dbContext.Villas;

        return View(villas);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Villa villa)
    {
        if (villa.Name == villa.Description)
        {
            ModelState.AddModelError("name", "The description cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Villas.Add(villa);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return View(villa);
    }

    public IActionResult Update(int villaId)
    {
        Villa? villa = _dbContext.Villas.FirstOrDefault(x => x.Id == villaId);

        if (villa is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Update(Villa villa)
    {
        if (ModelState.IsValid && villa.Id > 0)
        {
            _dbContext.Villas.Update(villa);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return View(villa);
    }

    public IActionResult Delete(int villaId)
    {
        Villa? villa = _dbContext.Villas.FirstOrDefault(x => x.Id == villaId);

        if (villa is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villa);
    }


    //[HttpPost]
    //public IActionResult Delete(Villa obj)
    //{
    //    bool deleted = _villaService.DeleteVilla(obj.Id);
    //    if (deleted)
    //    {
    //        TempData["success"] = "The villa has been deleted successfully.";
    //        return RedirectToAction(nameof(Index));
    //    }
    //    else
    //    {
    //        TempData["error"] = "Failed to delete the villa.";
    //    }
    //    return View();
    //}

}
