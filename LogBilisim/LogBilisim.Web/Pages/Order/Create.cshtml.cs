using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class CreateModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Order CreateOrder { get; set; }

    public SelectList CustomerList { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "YENÝ SÝPARÝÞ";

        var customers = await dbContext.Customers
            .OrderBy(x => x.Name)
            .ToListAsync();

        CustomerList = new SelectList(customers,"Id","Name");

    }

    public async Task<IActionResult> OnPostAsync()
    {
        dbContext.Entry<Models.Order>(new Models.Order
        {
            CustomerId = CreateOrder.CustomerId,
            OrderDate = CreateOrder.OrderDate,
            OrderStatus = CreateOrder.OrderStatus
        }).State = EntityState.Added;

        await dbContext.SaveChangesAsync();
        notyfService.Success("Sipariþ oluþturuldu.");
        return RedirectToPage("/Order/Index");
    }
}
