using LogBilisim.Web.Models.Abstracts;

namespace LogBilisim.Web.Models;

public class OrderDetail : AbstractModel
{
    public string OrderId { get; set; } 
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Amount { get; set; }

    public Product? Product { get; set; }
    public Order? Order { get; set; }

}
