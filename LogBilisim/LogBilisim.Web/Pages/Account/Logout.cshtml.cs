using LogBilisim.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogBilisim.Web.Pages.Account;

public class LogoutModel(SignInManager<User> signInManager) : PageModel
{
    public async Task<IActionResult> OnGetAsync()
    {
        await signInManager.SignOutAsync();
        return RedirectToPage("/Index");
    }
}
