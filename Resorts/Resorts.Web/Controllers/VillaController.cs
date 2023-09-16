using Microsoft.AspNetCore.Mvc;
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

}
