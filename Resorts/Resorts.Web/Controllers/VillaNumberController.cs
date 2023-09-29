using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resorts.Application.Common.Interfaces;
using Resorts.Infrastructure.Data;
using Resorts.Web.ViewModels;

namespace Resorts.Web.Controllers;

public class VillaNumberController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public IActionResult Index()
    {
        var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");

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

        bool roomNumberExists = _unitOfWork.VillaNumber.Any(r => r.Villa_Number == villaNumberVM.VillaNumber!.Villa_Number);

        if (ModelState.IsValid && !roomNumberExists)
        {
            _unitOfWork.VillaNumber.Add(villaNumberVM.VillaNumber!);
            _unitOfWork.Save();

            TempData["success"] = "The Villa Number has been created successfully.";

            return RedirectToAction(nameof(Index));
        }

        if (roomNumberExists)
        {
            TempData["error"] = "The Villa Number already exists.";
        }

        villaNumberVM.VillaList = GetVillaList();

        return View(villaNumberVM);
    }

    public IActionResult Update(int villaNumberId)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = GetVillaList(),
            VillaNumber = _dbContext.VillaNumbers.FirstOrDefault(r => r.Villa_Number == villaNumberId)
        };

        if (villaNumberVM.VillaNumber is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Update(VillaNumberVM villaNumberVM)
    {
        if (ModelState.IsValid)
        {
            _dbContext.VillaNumbers.Update(villaNumberVM.VillaNumber!);
            _dbContext.SaveChanges();

            TempData["success"] = "The Villa Number has been updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        villaNumberVM.VillaList = GetVillaList();

        return View(villaNumberVM);
    }

    public IActionResult Delete(int villaNumberId)
    {
        VillaNumberVM villaNumberVM = new()
        {
            VillaList = GetVillaList(),
            VillaNumber = _dbContext.VillaNumbers.FirstOrDefault(r => r.Villa_Number == villaNumberId)
        };

        if (villaNumberVM.VillaNumber is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villaNumberVM);
    }

    [HttpPost]
    public IActionResult Delete(VillaNumberVM villaNumberVM)
    {
        var existingVillaNumber = _dbContext.VillaNumbers.FirstOrDefault(r => r.Villa_Number == villaNumberVM.VillaNumber!.Villa_Number);

        if (existingVillaNumber is not null)
        {
            _dbContext.VillaNumbers.Remove(existingVillaNumber);
            _dbContext.SaveChanges();

            TempData["success"] = "The Villa Number has been deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = "The Villa Number could not be deleted.";

        return View(villaNumberVM);
    }

    private IEnumerable<SelectListItem> GetVillaList()
    {
        return _unitOfWork.Villa.GetAll().Select(r => new SelectListItem
        {
            Text = r.Name,
            Value = $"{r.Id}",
        });
    }

}
