using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Order;

public class AddModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public Models.OrderDetail AddOrderDetail { get; set; } = new();

    public SelectList ProductList { get; set; }

    public async Task OnGetAsync(string orderId)
    {
        var products = await dbContext.Products
            .OrderBy(x => x.Name)
            .ToListAsync();

        TempData["orderId"] = orderId;

        ProductList = new SelectList(products, "Id", "Name");
    }

    public async Task<IActionResult>OnPostAsync()
    {
        var orderDetail = new Models.OrderDetail
        {
            OrderId = AddOrderDetail.OrderId,
            ProductId = AddOrderDetail.ProductId,
            Quantity = AddOrderDetail.Quantity,
            Amount = dbContext.Products.SingleOrDefault(x => x.Id == AddOrderDetail.ProductId)!.Price * AddOrderDetail.Quantity
        };
        
        
        dbContext.Entry<Models.OrderDetail>(orderDetail).State = EntityState.Added;

        await dbContext.SaveChangesAsync();

        return RedirectToPage("/Order/Details", new {id= orderDetail.OrderId});
    }
}
