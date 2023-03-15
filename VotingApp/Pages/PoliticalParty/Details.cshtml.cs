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
    public class DetailsModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public DetailsModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

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
    }
}
