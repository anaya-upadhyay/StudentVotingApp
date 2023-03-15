using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.VotingCandidates
{
    public class DetailsModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public DetailsModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        public Candidate Candidate { get; set; } = new();
        public IList<Party> Parties { get; set; }
        public IList<Position> Positions { get; set; }
        public IList<Campus> Colleges { get; set; }

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
                Parties = _context.Parties.ToList();
                Positions = _context.Positions.ToList();
                Colleges = _context.Colleges.Select(p => new Campus { Id = p.Id, Name = p.Name }).ToList();

                Candidate.Id = candidate.Id;
                Candidate.Name = candidate.Name;
                Candidate.College = Colleges.FirstOrDefault(p => p.Id == candidate.CollegeId) == null
                        ? ""
                        : Colleges.First(p => p.Id == candidate.CollegeId).Name;
                Candidate.PartyName = Parties.FirstOrDefault(p => p.Id == candidate.PartyId) == null
                        ? ""
                        : Parties.First(p => p.Id == candidate.PartyId).DisplayName;
                Candidate.Position = Positions.FirstOrDefault(p => p.Id == candidate.PositionId) == null
                        ? ""
                        : Positions.First(p => p.Id == candidate.PositionId).DisplayName;
                Candidate.Symbol = candidate.Symbol;

                //Candidate = candidate;
            }
            return Page();
        }
    }
}
