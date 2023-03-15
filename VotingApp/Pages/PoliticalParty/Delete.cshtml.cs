using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.PoliticalParty
{
    public class DeleteModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public DeleteModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Party Party { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Parties == null)
            {
                return NotFound();
            }

            var party = await _context.Parties.FirstOrDefaultAsync(m => m.Id == id);

            if (party == null)
            {
                return NotFound();
            }
            else 
            {
                Party = party;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Parties == null)
            {
                return NotFound();
            }
            var party = await _context.Parties.FindAsync(id);

            if (party != null)
            {
                Party = party;
                _context.Parties.Remove(Party);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
