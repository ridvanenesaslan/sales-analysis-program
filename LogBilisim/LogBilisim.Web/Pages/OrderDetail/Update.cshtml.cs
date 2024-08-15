using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.OrderDetail;

public class UpdateModel(AppDbContext dbContext,INotyfService notyfService) : PageModel
{

    [BindProperty]
    public Models.OrderDetail UpdateOrderDetail { get; set; } = new();

    public SelectList ProductList { get; set; }

    public async Task OnGetAsync(string id)
    {
        var products = await dbContext.Products.ToListAsync();
        var orderDetail = await dbContext.OrderDetails.SingleOrDefaultAsync(x=>x.Id ==  id);

        TempData["orderId"] = orderDetail.OrderId;

        UpdateOrderDetail.Id = orderDetail.Id;
        UpdateOrderDetail.ProductId = orderDetail.ProductId;
        ProductList = new SelectList(products, "Id", "Name", orderDetail.ProductId);
        UpdateOrderDetail.Quantity = orderDetail.Quantity;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var orderDetail = await dbContext.OrderDetails.SingleOrDefaultAsync(x => x.Id == UpdateOrderDetail.Id);

        orderDetail.ProductId = UpdateOrderDetail.ProductId;
        orderDetail.Quantity = UpdateOrderDetail.Quantity;
        orderDetail.Amount = dbContext.Products.SingleOrDefault(x => x.Id == UpdateOrderDetail.ProductId)!.Price * UpdateOrderDetail.Quantity;

        dbContext.Entry<Models.OrderDetail>(orderDetail).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        notyfService.Success("Sipariþ detayý güncellendi.");
        return RedirectToPage("/Order/Details", new {id=orderDetail.OrderId});
    }
}
