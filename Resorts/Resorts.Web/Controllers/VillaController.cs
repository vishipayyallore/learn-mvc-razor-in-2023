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
            return NotFound();
        }

        return View(villa);
    }

    //[HttpPost]
    //public IActionResult Update(Villa obj)
    //{
    //    if (ModelState.IsValid && obj.Id > 0)
    //    {

    //        _villaService.UpdateVilla(obj);
    //        TempData["success"] = "The villa has been updated successfully.";
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View();
    //}

    //public IActionResult Delete(int villaId)
    //{
    //    Villa? obj = _villaService.GetVillaById(villaId);
    //    if (obj is null)
    //    {
    //        return RedirectToAction("Error", "Home");
    //    }
    //    return View(obj);
    //}


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
