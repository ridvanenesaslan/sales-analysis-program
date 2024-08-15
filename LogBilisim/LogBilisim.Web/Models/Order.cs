using LogBilisim.Web.Models.Abstracts;
using LogBilisim.Web.Models.Enums;

namespace LogBilisim.Web.Models;

public class Order : AbstractModel
{
    public string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatusType OrderStatus { get; set; }

    public Customer? Customer { get; set; }
    public ICollection<OrderDetail>OrderDetails { get; set; } = new HashSet<OrderDetail>();
    public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();




}
