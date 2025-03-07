using BSD2_24.Data;
using Microsoft.EntityFrameworkCore;

namespace BSD2_24.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BSD2_24Context(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BSD2_24Context>>()))
            {
                if (context.Ticket.Any() || context.Utilisateur.Any())
                {
                    return;
                }

                Status statusEnCours = new Status()
                {
                    Nom = "EnCours"
                };

                Status statusFini = new Status()
                {
                    Nom = "Fini"
                };

                context.Status.AddRange(
                    statusEnCours,
                    statusFini
                );

                Utilisateur user1 = new Utilisateur()
                {
                    Email = "a@a.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("toto"),
                    Role = "Admin"
                };

                Utilisateur user2 = new Utilisateur()
                {
                    Email = "b@b.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("toto"),
                    Role = "User"
                };

                context.Utilisateur.AddRange(user1, user2);

                context.Ticket.AddRange(
                    new Ticket
                    {
                        Title = "Kebab",
                        Description = "Délicieux sandwich grec.",
                        Status = statusEnCours,
                        Utilisateur = user1
                    },
                    new Ticket
                    {
                        Title = "Pizza",
                        Description = "Plat italien populaire.",
                        Status = statusEnCours,
                        Utilisateur = user1
                    },
                    new Ticket
                    {
                        Title = "Hot Dog",
                        Description = "Sandwich américain classique.",
                        Status = statusEnCours,
                        Utilisateur = user2
                    },
                    new Ticket
                    {
                        Title = "Donuts",
                        Description = "Beignet sucré.",
                        Status = statusEnCours,
                        Utilisateur = user2
                    },
                    new Ticket
                    {
                        Title = "Chapeau",
                        Description = "Accessoire de mode pour la tête.",
                        Status = statusFini,
                        Utilisateur = user1
                    },
                    new Ticket
                    {
                        Title = "Chemise",
                        Description = "Vêtement classique.",
                        Status = statusFini,
                        Utilisateur = user2
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
