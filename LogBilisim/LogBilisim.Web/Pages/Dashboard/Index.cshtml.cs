using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Dashboard;

public class IndexModel(AppDbContext dbContext) : PageModel
{
    [BindProperty]
    public int CustomerCount { get; set; }

    [BindProperty]
    public int ProductCount { get; set; }

    [BindProperty]
    public int CategoryCount { get; set; }

    [BindProperty]
    public decimal TotalOrder { get; set; }


    public async Task OnGetAsync()
    {
        ViewData["pageName"] = "ÝSTATÝSTÝKLER";

        CustomerCount = await dbContext.Customers.CountAsync();
        ProductCount = await dbContext.Products.CountAsync();
        CategoryCount = await dbContext.Categories.CountAsync();
        TotalOrder = await dbContext.Payments.SumAsync(x => x.PaymentAmount);
    }

    public async Task<IActionResult> OnPostAsync(string year)
    {
        var data = new List<object>();

        var orders = await dbContext.Orders.Where(x=>x.OrderDate.Year.ToString() == year).GroupBy(x => new ChartResponse
        {
            Month = x.OrderDate.Month.ToString(),
            Quantity = x.OrderDetails.Sum(x => x.Quantity)
        }).ToListAsync();

        List<string> labels = orders.Select(x => x.Key.Month).ToList();
        data.Add(labels);


        List<int> totalQuantity = orders.Select(x => x.Key.Quantity).ToList();


        data.Add(totalQuantity);

        return new JsonResult(data);

    }




}

public class ChartResponse
{
    public string Month { get; set; }
    public int Quantity { get; set; }
}


