﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.VotingCandidates
{
    public class DeleteModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public DeleteModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Candidate Candidate { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }

            var candidate = await _context.Candidate.FirstOrDefaultAsync(m => m.Id == id);

            if (candidate == null)
            {
                return NotFound();
            }
            else 
            {
                var colleges = _context.Colleges.Select(p => new Campus { Id = p.Id, Name = p.Name }).ToList();
                if (colleges.Any())
                {
                    candidate.College = colleges.FirstOrDefault(p => p.Id == candidate.CollegeId) == null
                        ? ""
                        : colleges.First(p => p.Id == candidate.CollegeId).Name;
                }

                Candidate = candidate;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Candidate == null)
            {
                return NotFound();
            }
            var candidate = await _context.Candidate.FindAsync(id);

            if (candidate != null)
            {
                Candidate = candidate;
                _context.Candidate.Remove(Candidate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
