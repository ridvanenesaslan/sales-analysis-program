using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class CreateModel(AppDbContext dbContext, INotyfService notyf) : PageModel
{

    [BindProperty]
    public Models.Customer CustomerCreate { get; set; } = new();
    public void OnGet()
    {
        ViewData["pageName"] = "YEN� M��TER�";

    }

    public async Task<IActionResult>OnPostAsync()
    {
 
        if(string.IsNullOrWhiteSpace(CustomerCreate.Name))
        {
            notyf.Warning("M��teri isim/unvan alan� bo� olamaz!");
            return Page();
        }

        var customer = new Models.Customer
        {
            Name = CustomerCreate.Name,
            PhoneNumber = CustomerCreate.PhoneNumber,
            EmailAddress = CustomerCreate.EmailAddress,
            Address = CustomerCreate.Address,
            City = CustomerCreate.City
        };

        dbContext.Entry<Models.Customer>(customer).State = EntityState.Added;
        await dbContext.SaveChangesAsync();
        notyf.Success("M��teri olu�turuldu.");
        return RedirectToPage("/Customer/Index");
    }
}
