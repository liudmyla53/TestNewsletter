using Microsoft.EntityFrameworkCore;
using TestUAA2.Models;

namespace TestUAA2
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscriber>()
                .Property(s => s.Themes)
                .HasConversion(
                    // Conversion de la liste en JSON pour stockage en base
                    v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions)null),
                    // Conversion du JSON en List<string> lors de la lecture
                    v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (System.Text.Json.JsonSerializerOptions)null) ?? new List<string>()
                );
        }
    }
}
      
