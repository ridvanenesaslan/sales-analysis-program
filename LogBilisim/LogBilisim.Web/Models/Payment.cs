using LogBilisim.Web.Models.Abstracts;
using LogBilisim.Web.Models.Enums;

namespace LogBilisim.Web.Models;

public class Payment : AbstractModel
{
    public DateTime PaymentDate { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal PaymentAmount { get; set; }
    public string OrderId { get; set; }
    public Order Order { get; set; }

}
