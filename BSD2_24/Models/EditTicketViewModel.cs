using Microsoft.AspNetCore.Mvc.Rendering;

namespace BSD2_24.Models
{
    public class EditTicketViewModel
    {
        public Ticket Ticket { get; set; }

        public List<SelectListItem> Status { get; set; }
    }
}
