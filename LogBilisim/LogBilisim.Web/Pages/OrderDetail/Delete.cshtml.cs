using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.OrderDetail;

public class DeleteModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync(string id)
    {
       
        var orderDetail = await dbContext.OrderDetails.SingleOrDefaultAsync(x => x.Id == id);
        TempData["id"] = id;
        TempData["orderId"] = orderDetail.OrderId;
        ViewData["message"] = "Kayýt silinecek! Onaylýyor musunuz?";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var orderDetail = await dbContext.OrderDetails.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());

        string orderId = orderDetail.OrderId;

        dbContext.Entry<Models.OrderDetail>(orderDetail).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"Sipariþ detayý silindi.");

        return RedirectToPage("/Order/Details", new {id= orderId });
    }
}
