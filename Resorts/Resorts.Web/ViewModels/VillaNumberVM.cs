using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resorts.Domain.Entities;

namespace Resorts.Web.ViewModels;

public class VillaNumberVM
{
    public VillaNumber? VillaNumber { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? VillaList { get; set; }
}
