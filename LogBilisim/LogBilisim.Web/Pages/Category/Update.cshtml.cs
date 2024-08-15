using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Category;

public class UpdateModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Category UpdateCategory { get; set; } = new();

    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "KATEGORÝ GÜNCELLEME";
        var category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
        UpdateCategory.Id = id;
        UpdateCategory.Name = category.Name;
        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == UpdateCategory.Id);
        category.Name = UpdateCategory.Name;

        dbContext.Entry<Models.Category>(category).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"Kategori güncellendi.");

        return RedirectToPage("/Category/Index");
    }
}
