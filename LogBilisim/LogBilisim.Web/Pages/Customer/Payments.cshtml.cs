using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class PaymentsModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public Models.Customer? Customer { get; set; } = new();
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "MÜÞTERÝ ÖDEMELERÝ";
        Customer = await dbContext.Customers
            .Include(x=>x.Orders)
            .ThenInclude(x=>x.Payments)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
