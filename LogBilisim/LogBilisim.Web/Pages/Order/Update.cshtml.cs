using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class UpdateModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Order UpdateOrder { get; set; } = new();

    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "SÝPARÝÞ GÜNCELLEME";
        var order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);
        UpdateOrder.Id = order.Id;
        UpdateOrder.OrderStatus = order.OrderStatus;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Id == UpdateOrder.Id);
        order.OrderStatus = UpdateOrder.OrderStatus;

        dbContext.Entry<Models.Order>(order).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        notyfService.Success("Sipariþ güncellendi.");
        return RedirectToPage("/Order/Index");
    }
}
