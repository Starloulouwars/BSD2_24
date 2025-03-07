using Microsoft.Extensions.Hosting;

namespace BSD2_24.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    }
}
