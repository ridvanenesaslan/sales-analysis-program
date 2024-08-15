using LogBilisim.Web.Models.Abstracts;

namespace LogBilisim.Web.Models;

public class Customer : AbstractModel
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public ICollection<Order>Orders { get; set; } = new HashSet<Order>();

}
