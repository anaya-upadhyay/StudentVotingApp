﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.VotingCandidates
{
    public class EditModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public EditModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Candidate Candidate { get; set; } = default!;
        public IEnumerable<SelectListItem> Parties { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Colleges { get; set; } = new List<SelectListItem>();


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }

            Parties = GetParties();
            Positions = GetPositions();
            Colleges = GetColleges();

            var candidate =  await _context.Candidate.FirstOrDefaultAsync(m => m.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }
            Candidate = candidate;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Candidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(Candidate.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CandidateExists(int id)
        {
          return (_context.Candidate?.Any(e => e.Id == id)).GetValueOrDefault();
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
