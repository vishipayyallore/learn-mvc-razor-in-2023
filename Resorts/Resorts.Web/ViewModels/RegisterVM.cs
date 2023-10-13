using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Resorts.Web.ViewModels;

public class RegisterVM
{
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    [Display(Name = "Confirm password")]
    public required string ConfirmPassword { get; set; }

    public required string Name { get; set; }

    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    public string? RedirectUrl { get; set; }

    public string? Role { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem>? RoleList { get; set; }
}