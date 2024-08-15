using AspNetCoreHero.ToastNotification.Abstractions;
using LogBilisim.Web.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LogBilisim.Web.Pages.Customer;

public class UpdateModel(AppDbContext dbContext, INotyfService notyfService) : PageModel
{
    [BindProperty]
    public Models.Customer? CustomerUpdate { get; set; } = new();


    public async Task OnGetAsync(string id)
    {
        ViewData["pageName"] = "MÜÞTERÝ GÜNCELLEME";

        var customer = await dbContext.Customers.SingleOrDefaultAsync(_ => _.Id == id);

        CustomerUpdate.Id = customer.Id;
        CustomerUpdate.Name = customer.Name;
        CustomerUpdate.PhoneNumber = customer.PhoneNumber;
        CustomerUpdate.EmailAddress = customer.EmailAddress;
        CustomerUpdate.City = customer.City;
        CustomerUpdate.Address = customer.Address;
    }

    public async Task<IActionResult>OnPostAsync()
    {
        var customer = await dbContext.Customers.SingleOrDefaultAsync(_ => _.Id == CustomerUpdate.Id);

        customer.Name = CustomerUpdate.Name;
        customer.PhoneNumber = CustomerUpdate.PhoneNumber;
        customer.EmailAddress = CustomerUpdate.EmailAddress;
        customer.Address = CustomerUpdate.Address;
        customer.City = CustomerUpdate.City;

        dbContext.Entry<Models.Customer>(customer).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
        notyfService.Success($"{customer.Name} bilgileri güncellendi.");
        return RedirectToPage("/Customer/Index");
    }
}
