using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using LogBilisim.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Payment;

public class CreateModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{

    [BindProperty]
    public Models.Payment CreatePayment { get; set; } = new();

    public async Task OnGetAsync(string orderId)
    {
        TempData["orderId"] = orderId;
        var order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Id == orderId);
        TempData["customerId"] = order.CustomerId;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        CreatePayment.OrderId = TempData["orderId"]!.ToString()!;

        var payment = new Models.Payment
        {
            OrderId = CreatePayment.OrderId,
            PaymentDate = CreatePayment.PaymentDate,
            PaymentAmount = CreatePayment.PaymentAmount,
            PaymentType = CreatePayment.PaymentType
        };

        dbContext.Entry<Models.Payment>(payment).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        await dbContext.SaveChangesAsync();
        var order = await dbContext.Orders.SingleOrDefaultAsync(x => x.Id == payment.OrderId);

        notyfService.Success("Ödeme kaydý oluþturuldu.");

        return RedirectToPage("/Customer/Payments", new { id = order.CustomerId});

    }
}
