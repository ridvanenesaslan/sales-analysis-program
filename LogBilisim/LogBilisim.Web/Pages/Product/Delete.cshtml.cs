using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Product;

public class DeleteModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "ÜRÜN SÝLME";

        var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        TempData["id"] = id;
        ViewData["message"] = $"{product.Name} silinecek! Onaylýyor musunuz?";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());
        string productName = product.Name;

        dbContext.Entry<Models.Product>(product).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"{productName} silindi.");

        return RedirectToPage("/Product/Index");
    }
}
