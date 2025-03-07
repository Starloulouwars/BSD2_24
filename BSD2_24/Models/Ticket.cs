using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace BSD2_24.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int? StatusId { get; set; }
        public Status? Status { get; set; } = null!;


        public int? UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; } = null!;

    }
}
