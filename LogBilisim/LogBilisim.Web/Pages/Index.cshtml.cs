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
            ViewData["validationError"] = "Kullanýcý adý yada þifre boþ olamaz!";
            return Page();
        }


        var user = await userManager.FindByNameAsync(LoginViewModel.Username);
        if (user is null)
        {
            ViewData["notExistUserError"] = "Kullanýcý bulunamadý!";
            return Page();
        }

        var result = await signInManager.PasswordSignInAsync(user, password: LoginViewModel.Password, isPersistent: LoginViewModel.RememberMe, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            ViewData["passwordError"] = "Þifre hatalý!";
            return Page();
        }

        return RedirectToPage("/Dashboard/Index");
    }
}
