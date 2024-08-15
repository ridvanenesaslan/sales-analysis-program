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
        ViewData["pageName"] = "YENÝ KATEGORÝ";

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if(string.IsNullOrWhiteSpace(CreateCategory.Name))
        {
            notyfService.Error("Kategori adý boþ olamaz!");
            return Page();
        }

        dbContext.Entry<Models.Category>(new Models.Category
        {
            Name = CreateCategory.Name
        }).State = Microsoft.EntityFrameworkCore.EntityState.Added;

        var result = await dbContext.SaveChangesAsync();

        if (result == 0)
        {
            notyfService.Error("Bir hata oluþtu!");
            return Page();
        }

        notyfService.Success("Kategori oluþturuldu.");
        return RedirectToPage("/Category/Index");

    }




}
