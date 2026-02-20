namespace TestUAA2.Models
{
    public class Subscriber
    {
        public int Id { get; set; }  // Clé primaire (identifiant unique)
        public string Email { get; set; } = string.Empty;    // Adresse e-mail de l'abonné
        public List<string> Themes { get; set; } = new ();     // Liste des thèmes choisis par l'utilisateur

    }
}
