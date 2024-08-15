using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Product;

public class CreateModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Product CreateProduct { get; set; } = new();

    public SelectList CategoryList { get; set; }

    public async Task OnGetAsync()
    {
        var categories = await dbContext.Categories
            .OrderBy(x => x.Name)
            .ToListAsync();

        CategoryList = new SelectList(categories,"Id","Name");
    }

    public async Task<IActionResult>OnPostAsync()
    {
        dbContext.Entry<Models.Product>(new Models.Product
        {
            CategoryId = CreateProduct.CategoryId,
            Name = CreateProduct.Name,
            Description = CreateProduct.Description,
            Price = CreateProduct.Price,
            StockQuantity = CreateProduct.StockQuantity

        }).State = EntityState.Added;

        await dbContext.SaveChangesAsync();

        notyfService.Success("Yeni ürün oluþturuldu.");

        return RedirectToPage("/Product/Index");
    }
}
