using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class DeleteModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "S�PAR�� S�LME";
        var order = await dbContext.Orders.SingleOrDefaultAsync(x=>x.Id == id);
        TempData["id"] = id;
        ViewData["message"] = "Kay�t silinecek! Onayl�yor musunuz?";
    }

    public async Task<IActionResult>OnPostAsync()
    {
        var order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());

        dbContext.Entry<Models.Order>(order).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"Sipari� silindi.");

        return RedirectToPage("/Order/Index");
    }
}
