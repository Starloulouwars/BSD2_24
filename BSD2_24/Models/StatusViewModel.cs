using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.CodeAnalysis;

namespace BSD2_24.Models
{
    public class StatusViewModel
    {
        public List<Ticket>? Tickets { get; set; }
        public string? Recherche { get; set; }

        public string? nom {  get; set; }
        public SelectList Status { get; set; }

        public string? StatusSelectionne { get; set; }
    }
}
