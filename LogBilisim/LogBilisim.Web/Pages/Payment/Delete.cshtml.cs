using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Payment;

public class DeleteModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "�DEME S�LME";

        var payment = await dbContext.Payments.SingleOrDefaultAsync(x => x.Id == id);
        TempData["id"] = id;
        ViewData["message"] = $"{payment.Id} nolu �deme silinecek! Onayl�yor musunuz?";

    }

    public async Task<IActionResult> OnPostAsync()
    {
        var payment = await dbContext.Payments.SingleOrDefaultAsync(x => x.Id == TempData["id"]!.ToString());
        string paymentNo = payment.Id;

        dbContext.Entry<Models.Payment>(payment).State = EntityState.Deleted;
        await dbContext.SaveChangesAsync();

        notyfService.Success($"{paymentNo} nolu �deme silindi.");

        return RedirectToPage("/Payment/Index");
    }
}
