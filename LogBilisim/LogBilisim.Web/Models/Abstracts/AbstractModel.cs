namespace LogBilisim.Web.Models.Abstracts;

public class AbstractModel : IModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
