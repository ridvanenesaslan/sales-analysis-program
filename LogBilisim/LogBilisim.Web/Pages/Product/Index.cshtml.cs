using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Product;

public class IndexModel(AppDbContext dbContext) : PageModel
{

    [BindProperty]
    public List<Models.Product>Products { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "ÜRÜNLER";
        Products = await dbContext.Products
            .Include(x=>x.Category)
            .OrderBy(x => x.Name).ToListAsync();
    }
}
