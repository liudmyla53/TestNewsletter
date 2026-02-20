using System.ComponentModel.DataAnnotations;

namespace TestUAA2.Models
{
    public class SubscriptionForm
    {
        [Required] // Champ obligatoire
        [EmailAddress] // Validation du format e-mail
        public string Email { get; set; }=string.Empty;

        [Required(ErrorMessage = "Choisissez au moins un thème.")]
        // Au moins un thème doit être sélectionné
        public List<string> Themes { get; set; } = new();

    }
}
