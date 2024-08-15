using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class OrdersModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public List<Models.Order> Orders { get; set; } = new();
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "MÜÞTERÝ SÝPARÝÞLERÝ";
        Orders = await dbContext.Orders
            .Where(x => x.CustomerId == id)
            .Include(x=>x.OrderDetails)
            .ToListAsync();
    }
}
