using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Models;
using LogBilisim.Web.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogBilisim.Web.Pages;

public class IndexModel(UserManager<User> userManager, SignInManager<User> signInManager) : PageModel
{
    [BindProperty]
    public LoginViewModel LoginViewModel { get; set; } = new();

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
       

        if(string.IsNullOrWhiteSpace(LoginViewModel.Username) || string.IsNullOrWhiteSpace(LoginViewModel.Password))
        {
            ViewData["validationError"] = "Kullan�c� ad� yada �ifre bo� olamaz!";
            return Page();
        }


        var user = await userManager.FindByNameAsync(LoginViewModel.Username);
        if (user is null)
        {
            ViewData["notExistUserError"] = "Kullan�c� bulunamad�!";
            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(user, password: LoginViewModel.Password, isPersistent: LoginViewModel.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            ViewData["passwordError"] = "�ifre hatal�!";
            return Page();
        }

        return RedirectToPage("/Dashboard/Index");
    }
}
