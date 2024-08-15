using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Payment;

public class DetailsModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public Models.Payment? Payment { get; set; } = new();

    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "ÖDEME DETAY";
        Payment = await dbContext.Payments
            .Include(x => x.Order)
            .ThenInclude(x => x.Customer)
            .SingleOrDefaultAsync(x => x.Id == id);

    }
}
