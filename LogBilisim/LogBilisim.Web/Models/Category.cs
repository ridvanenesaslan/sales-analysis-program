using LogBilisim.Web.Models.Abstracts;

namespace LogBilisim.Web.Models;

public class Category : AbstractModel
{
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

}
