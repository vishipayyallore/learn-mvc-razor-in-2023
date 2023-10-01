using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resorts.Application.Common.Interfaces;
using Resorts.Web.ViewModels;

namespace Resorts.Web.Controllers;

public class AmenityController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public IActionResult Index()
    {
        var amenities = _unitOfWork.Amenity.GetAll(includeProperties: "Villa");

        return View(amenities);
    }

    public IActionResult Create()
    {
        AmenityVM amenityVM = new()
        {
            VillaList = GetVillaList()
        };

        return View(amenityVM);
    }

    [HttpPost]
    public IActionResult Create(AmenityVM amenityVM)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Amenity.Add(amenityVM.Amenity!);
            _unitOfWork.Save();

            TempData["success"] = "The Amenity has been created successfully.";

            return RedirectToAction(nameof(Index));
        }

        amenityVM.VillaList = GetVillaList();

        return View(amenityVM);
    }

    public IActionResult Update(int amenityId)
    {
        AmenityVM amenityVM = new()
        {
            VillaList = GetVillaList(),
            Amenity = _unitOfWork.Amenity.Get(r => r.Id == amenityId)
        };

        return amenityVM.Amenity is null ? RedirectToAction("Error", "Home") : View(amenityVM);
    }

    [HttpPost]
    public IActionResult Update(AmenityVM amenityVM)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Amenity.Update(amenityVM.Amenity!);
            _unitOfWork.Save();

            TempData["success"] = "The Amenity has been updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        amenityVM.VillaList = GetVillaList();

        return View(amenityVM);
    }

    public IActionResult Delete(int amenityId)
    {
        AmenityVM amenityVM = new()
        {
            VillaList = GetVillaList(),
            Amenity = _unitOfWork.Amenity.Get(r => r.Id == amenityId)
        };

        return amenityVM.Amenity is null ? RedirectToAction("Error", "Home") : View(amenityVM);
    }

    [HttpPost]
    public IActionResult Delete(AmenityVM AmenityVM)
    {
        var existingAmenity = _unitOfWork.Amenity.Get(r => r.Id == AmenityVM.Amenity!.Id);

        if (existingAmenity is not null)
        {
            _unitOfWork.Amenity.Remove(existingAmenity);
            _unitOfWork.Save();

            TempData["success"] = "The Amenity has been deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = "The Amenity could not be deleted.";

        return View(AmenityVM);
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
