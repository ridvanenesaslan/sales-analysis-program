using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class IndexModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public List<Models.Order>Orders { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "SÝPARÝÞLER";

        Orders = await dbContext.Orders
            .Include(x=>x.Customer)
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();
    }
}
