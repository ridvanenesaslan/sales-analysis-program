using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Payment;

public class UpdateModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Payment UpdatePayment { get; set; } = new();

    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "ÖDEME GÜNCELLEME";
        var payment = await dbContext.Payments.SingleOrDefaultAsync(x=>x.Id == id);

        UpdatePayment.Id = payment.Id;
        UpdatePayment.PaymentDate = payment.PaymentDate;
        UpdatePayment.PaymentType = payment.PaymentType;
        UpdatePayment.PaymentAmount = payment.PaymentAmount;
    }

    public async Task<IActionResult>OnPostAsync()
    {
        var payment = await dbContext.Payments.SingleOrDefaultAsync(x => x.Id == UpdatePayment.Id);
        payment.PaymentDate = UpdatePayment.PaymentDate;
        payment.PaymentType = UpdatePayment.PaymentType;
        payment.PaymentAmount = UpdatePayment.PaymentAmount;

        dbContext.Entry<Models.Payment>(payment).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();

        notyfService.Success("Ödeme bilgisi güncellendi.");
        
        return RedirectToPage("/Payment/Index");
    }
}
