using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Payment;

public class IndexModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public List<Models.Payment> Payments { get; set; } = new();
    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "ÖDEMELER";
        Payments = await dbContext.Payments
            .OrderByDescending(x=>x.PaymentDate)
            .Include(x=>x.Order)
            .ThenInclude(x=>x.Payments)
            .ToListAsync();
    }
}
