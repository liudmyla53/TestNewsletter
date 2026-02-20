using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestUAA2.Models;
using TestUAA2.Services;

namespace TestUAA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        // Simulation DB statique
        /* private static List<Subscriber> _db = new();
         private readonly IEmailService _email;

         public NewsletterController(IEmailService email) => _email = email;*/
        private readonly AppDbContext _context; // Contexte de base de données
        private readonly IEmailService _email; // Service d'envoi d'e-mails

        // Injection des dépendances via le constructeur
        public NewsletterController(AppDbContext context, IEmailService email)
        {
            _context = context;
            _email = email;
        }

        // Endpoint POST : api/newsletter/subscribe
        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe(SubscriptionForm form)
        {
            // Vérifie si l'utilisateur existe déjà dans la base de données
            var existing = await _context.Subscribers
            .FirstOrDefaultAsync(s => s.Email.ToLower() == form.Email.ToLower());

            if (existing != null)
            {
                // Si l'utilisateur existe, on met à jour les thèmes
                existing.Themes = form.Themes;
            }
            else
            {
                _context.Subscribers.Add(new Subscriber { Email = form.Email, Themes = form.Themes });
            }
            // Sauvegarde des modifications dans la base de données
            await _context.SaveChangesAsync();
            // Envoi d'un e-mail de confirmation
            try { await _email.SendAsync(form.Email, "Newsletter", "Vous êtes abonné !");}
            catch { }
            

            return Ok("Succes");
        }
        // Endpoint DELETE : 
        [HttpDelete("unsubscribe")]
        public async Task<IActionResult> Unsubscribe(string email)
        {
            // Recherche de l'utilisateur par e-mail
            var user = await _context.Subscribers.FirstOrDefaultAsync(s => s.Email.ToLower() == email.ToLower());
            if (user == null) return NotFound("Email non trouvé.");
            // Suppression de l'utilisateur (droit à l'oubli)
            _context.Subscribers.Remove(user); // Droit à l'oubli
                                               // Sauvegarde des changements
            await _context.SaveChangesAsync();
            // Envoi d'un e-mail de confirmation de suppression
            await _email.SendAsync(email, "Newsletter", "Toutes vos données ont été supprimées.");
            return Ok("Désabonné et supprimé.");
        }
        
    }
}
