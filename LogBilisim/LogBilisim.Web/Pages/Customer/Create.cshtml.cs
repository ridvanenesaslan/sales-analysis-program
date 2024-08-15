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
        ViewData["pageName"] = "YENÝ MÜÞTERÝ";

    }

    public async Task<IActionResult>OnPostAsync()
    {
 
        if(string.IsNullOrWhiteSpace(CustomerCreate.Name))
        {
            notyf.Warning("Müþteri isim/unvan alaný boþ olamaz!");
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
        notyf.Success("Müþteri oluþturuldu.");
        return RedirectToPage("/Customer/Index");
    }
}
