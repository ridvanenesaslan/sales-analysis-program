using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class DeleteModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "MÜÞTERÝ SÝLME";

        var customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
        TempData["id"] = id;
        ViewData["message"] = $"{customer.Name} silinecek! Onaylýyor musunuz?";

    }

    public async Task<IActionResult>OnPostAsync()
    {
        var customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());
        string customerName = customer.Name;

        dbContext.Entry<Models.Customer>(customer).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"{customerName} silindi.");

        return RedirectToPage("/Customer/Index");
    }
}
