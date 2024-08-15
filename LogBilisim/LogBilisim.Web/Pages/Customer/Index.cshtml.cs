using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class IndexModel(AppDbContext dbContext) : PageModel
{

    [BindProperty]
    public List<Models.Customer> Customers { get; set; } = new();

    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "MÜÞTERÝLER";
        Customers = await dbContext.Customers.ToListAsync();
    }
}
