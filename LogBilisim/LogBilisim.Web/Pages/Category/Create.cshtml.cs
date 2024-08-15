using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LogBilisim.Web.Pages.Category;

public class CreateModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{

    [BindProperty]
    public Models.Category CreateCategory { get; set; } = new();
    public void OnGet()
    {
        ViewData["pageName"] = "YEN� KATEGOR�";

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(string.IsNullOrWhiteSpace(CreateCategory.Name))
        {
            notyfService.Error("Kategori ad� bo� olamaz!");
            return Page();
        }

        dbContext.Entry<Models.Category>(new Models.Category
        {
            Name = CreateCategory.Name
        }).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        var result = await dbContext.SaveChangesAsync();

        if (result == 0)
        {
            notyfService.Error("Bir hata olu�tu!");
            return Page();
        }

        notyfService.Success("Kategori olu�turuldu.");
        return RedirectToPage("/Category/Index");

    }




}
