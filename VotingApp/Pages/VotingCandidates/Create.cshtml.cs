using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.VotingCandidates
{
    public class CreateModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public CreateModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            Parties = GetParties();
            Positions = GetPositions();
            Colleges = GetColleges();

            return Page();
        }

        [BindProperty]
        public Candidate Candidate { get; set; } = default!;
        
        public IEnumerable<SelectListItem> Parties { get; set; } = new  List<SelectListItem>();
        public IEnumerable<SelectListItem> Positions { get; set; } = new  List<SelectListItem>();
        public IEnumerable<SelectListItem> Colleges { get; set; } = new List<SelectListItem>();

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Candidate == null || Candidate == null)
            {
                return Page();
            }

            _context.Candidate.Add(Candidate);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private IEnumerable<SelectListItem> GetParties()
        {
            var parties = from p in _context.Parties
                         select new { Id = p.Id, Name = p.DisplayName };

            return new SelectList(parties, "Id", "Name");
        }

        private IEnumerable<SelectListItem> GetPositions()
        {
            var positions = from p in _context.Positions
                          select new { Id = p.Id, Name = p.DisplayName };

            return new SelectList(positions, "Id", "Name");
        }

        private IEnumerable<SelectListItem> GetColleges()
        {
            var colleges = from p in _context.Colleges
                            select new { Id = p.Id, Name = p.Name };

            return new SelectList(colleges, "Id", "Name");
        }
    }
}
