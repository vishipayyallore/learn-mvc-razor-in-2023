using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Resorts.Application.Common.Interfaces;
using Resorts.Domain.Entities;
using Resorts.Web.ViewModels;
using WhiteLagoon.Application.Common.Utility;

namespace Resorts.Web.Controllers;

public class AccountController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly RoleManager<IdentityRole> _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

    public IActionResult Login(string? returnUrl)
    {
        returnUrl ??= Url.Content("~/");

        LoginVM loginVM = new()
        {
            Email = string.Empty,
            Password = string.Empty,
            RedirectUrl = returnUrl,
        };

        return View(loginVM);
    }

    public IActionResult Register()
    {

        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).Wait();
        }

        RegisterVM registerVM = new()
        {
            Name = string.Empty,
            Email = string.Empty,
            Password = string.Empty,
            ConfirmPassword = string.Empty,
            RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            })
        };

        return View(registerVM);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVM)
    {
        ApplicationUser user = new()
        {
            Name = registerVM.Name,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
            NormalizedEmail = registerVM.Email.ToUpper(),
            EmailConfirmed = true,
            UserName = registerVM.Email,
            CreatedAt = DateTime.Now,
        };

        IdentityResult results = await _userManager.CreateAsync(user, registerVM.Password);

        if (results.Succeeded)
        {
            if (!string.IsNullOrEmpty(registerVM.Role))
            {
                _ = await _userManager.AddToRoleAsync(user, registerVM.Role);
            }
            else
            {
                _ = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);

            if (string.IsNullOrEmpty(registerVM.RedirectUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction(registerVM.RedirectUrl);
            }
        }

        return View(registerVM);
    }

}
