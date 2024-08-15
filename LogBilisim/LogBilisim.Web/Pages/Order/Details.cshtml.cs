using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class DetailsModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public List<Models.OrderDetail> OrderDetails { get; set; } = new();
    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "SÝPARÝÞ DETAY";
        TempData["orderId"] = id;
        OrderDetails = await dbContext.OrderDetails
            .Where(x => x.OrderId == id)
            .Include(x=>x.Product)
            .ToListAsync();

        TempData["totalAmount"] = OrderDetails.Sum(x => x.Amount);
    }
}
