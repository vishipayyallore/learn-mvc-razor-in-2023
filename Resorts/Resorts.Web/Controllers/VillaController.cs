using Microsoft.AspNetCore.Mvc;
using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;

namespace Resorts.Web.Controllers;

// Primary Constructor
public class VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));

    public IActionResult Index()
    {
        var villas = _unitOfWork.Villa.GetAll();

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

            if (villa.Image is not null)
            {
                const string imageFolderName = @"images\VillaImage";

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(villa.Image.FileName)}";
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imageFolderName);

                using FileStream fileStream = new(Path.Combine(imagePath, fileName), FileMode.Create);
                villa.Image.CopyTo(fileStream);

                villa.ImageUrl = $"{imageFolderName}\\{fileName}";
            }
            else
            {
                villa.ImageUrl = "https://placehold.co/600x400";
            }

            _unitOfWork.Villa.Add(villa);
            _unitOfWork.Villa.Save();

            TempData["success"] = "The Villa has been created successfully.";

            return RedirectToAction(nameof(Index));
        }

        return View(villa);
    }

    public IActionResult Update(int villaId)
    {
        Villa? villa = _unitOfWork.Villa.Get(x => x.Id == villaId);

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
            _unitOfWork.Villa.Update(villa);
            _unitOfWork.Villa.Save();

            TempData["success"] = "The Villa has been updated successfully.";

            return RedirectToAction(nameof(Index));
        }

        return View(villa);
    }

    public IActionResult Delete(int villaId)
    {
        Villa? villa = _unitOfWork.Villa.Get(x => x.Id == villaId);

        if (villa is null)
        {
            return RedirectToAction("Error", "Home");
        }

        return View(villa);
    }

    [HttpPost]
    public IActionResult Delete(Villa villa)
    {
        Villa? existingVilla = _unitOfWork.Villa.Get(x => x.Id == villa.Id);

        if (existingVilla is not null)
        {
            _unitOfWork.Villa.Remove(existingVilla);
            _unitOfWork.Villa.Save();

            TempData["success"] = "The Villa has been deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        TempData["error"] = "The Villa could not be deleted.";

        return View(villa);
    }

}
