using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.College
{
    public class DeleteModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public DeleteModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Campus Campus { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Colleges == null)
            {
                return NotFound();
            }

            var campus = await _context.Colleges.FirstOrDefaultAsync(m => m.Id == id);

            if (campus == null)
            {
                return NotFound();
            }
            else 
            {
                Campus = campus;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Colleges == null)
            {
                return NotFound();
            }
            var campus = await _context.Colleges.FindAsync(id);

            if (campus != null)
            {
                Campus = campus;
                _context.Colleges.Remove(Campus);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
