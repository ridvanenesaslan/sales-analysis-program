using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Product;

public class UpdateModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{

    [BindProperty]
    public Models.Product UpdateProduct { get; set; } = new();

    public SelectList CategoryList { get; set; }

    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "ÜRÜN GÜNCELLEME";

        var categories = await dbContext.Categories.ToListAsync();

        var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == id);
        UpdateProduct.Id = product.Id;
        UpdateProduct.Name = product.Name;
        UpdateProduct.Description = product.Description;
        UpdateProduct.Price = product.Price;
        UpdateProduct.StockQuantity = product.StockQuantity;
        CategoryList = new SelectList(categories, "Id", "Name", product.CategoryId);
    }

    public async Task<IActionResult> OnPostAsync()
    {
       
        var product = await dbContext.Products.SingleOrDefaultAsync(x => x.Id == UpdateProduct.Id);
        product.Name = UpdateProduct.Name;
        product.Description = UpdateProduct.Description;
        product.Price =UpdateProduct.Price;
        product.StockQuantity= UpdateProduct.StockQuantity;
        product.CategoryId = UpdateProduct.CategoryId;

        dbContext.Entry<Models.Product>(product).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();

        notyfService.Success("Ürün güncellendi.");
        return RedirectToPage("/Product/Index");
    }
}
