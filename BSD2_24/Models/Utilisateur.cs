using Microsoft.Extensions.Hosting;

namespace BSD2_24.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }

    }
}
