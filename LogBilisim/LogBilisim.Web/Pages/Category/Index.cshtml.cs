using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Category;

public class IndexModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public List<Models.Category>Categories { get; set; }
    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "KATEGORÝLER";
        Categories = await dbContext.Categories.ToListAsync();
    }
}
