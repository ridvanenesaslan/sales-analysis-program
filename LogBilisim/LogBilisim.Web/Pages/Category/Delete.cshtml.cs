using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Category
{
    public class DeleteModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
    {
        public async Task OnGetAsync(string id)
        {
            ViewData["pageName"] = "KATEGORÝ SÝLME";

            var category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
            TempData["id"] = id;
            ViewData["message"] = $"{category.Name} ile bu kategorideki ürünler de silinecek! Onaylýyor musunuz?";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());
            string categoryName = category.Name;

            dbContext.Entry<Models.Category>(category).State = EntityState.Deleted;
            await dbContext.SaveChangesAsync();

            notyfService.Success($"{categoryName} silindi.");

            return RedirectToPage("/Category/Index");
        }
    }
}
