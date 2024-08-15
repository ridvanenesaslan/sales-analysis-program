using LogBilisim.Web.Models.Abstracts;

namespace LogBilisim.Web.Models;

public class Product : AbstractModel
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string CategoryId { get; set; }
    public Category? Category { get; set; }


}
