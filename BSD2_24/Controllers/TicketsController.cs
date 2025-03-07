using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSD2_24.Data;
using BSD2_24.Models;

namespace BSD2_24.Controllers
{
    public class TicketsController : Controller
    {
        private readonly BSD2_24Context _context;

        public TicketsController(BSD2_24Context context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string StatusSelectionne, string recherche)
        {
            var requeteTickets = _context.Ticket.Include(t => t.Status).Include(t => t.Utilisateur).AsQueryable();

            IQueryable<string> requeteStatus = _context.Ticket
                .Where(t => t.Status != null)
                .OrderBy(t => t.Status.Nom)
                .Select(t => t.Status.Nom)
                .Distinct();

            if (!string.IsNullOrEmpty(recherche))
            {
                requeteTickets = requeteTickets
                    .Where(t => t.Title != null && t.Title.ToUpper().Contains(recherche.ToUpper()));
            }

            if (!string.IsNullOrEmpty(StatusSelectionne))
            {
                requeteTickets = requeteTickets
                    .Where(t => t.Status != null && t.Status.Nom.ToUpper() == StatusSelectionne.ToUpper());
            }

            var viewModel = new StatusViewModel
            {
                Recherche = recherche,
                Tickets = await requeteTickets.ToListAsync(),
                Status = new SelectList(await requeteStatus.ToListAsync())
            };

            return View(viewModel);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ticket = await _context.Ticket.Include(t => t.Status).Include(t => t.Utilisateur).FirstOrDefaultAsync(t => t.Id == id);
            if (ticket == null) return NotFound();

            var statusList = _context.Status.ToList();
            var statusSelectList = statusList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Nom
            }).ToList();

            return View(new EditTicketViewModel
            {
                Ticket = ticket,
                Status = statusSelectList
            });
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,StatusId,UtilisateurId")] Ticket ticket)
        {
            if (id != ticket.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var statusEntity = _context.Status.Find(ticket.StatusId);
                    var utilisateurEntity = _context.Utilisateur.Find(ticket.UtilisateurId);

                    if (statusEntity != null)
                    {
                        ticket.Status = statusEntity;
                    }

                    if (utilisateurEntity != null)
                    {
                        ticket.Utilisateur = utilisateurEntity;
                    }

                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var statusList = _context.Status.ToList();
            var statusSelectList = statusList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Nom
            }).ToList();

            return View(new EditTicketViewModel
            {
                Ticket = ticket,
                Status = statusSelectList
            });
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }
    }
}
