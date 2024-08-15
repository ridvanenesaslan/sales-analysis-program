using System.ComponentModel.DataAnnotations;

namespace LogBilisim.Web.ViewModels.CustomerViewModels;

public class CustomerCreate
{
    [Required(ErrorMessage ="Müşteri isim yada ünvan alanı boş olamaz!")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Telefon alanı boş olamaz!")]
    [Phone(ErrorMessage ="Format hatalı!")]
    public string PhoneNumber { get; set; }

    public string EmailAddress { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
