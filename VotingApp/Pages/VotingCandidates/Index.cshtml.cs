using System;
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
    public class IndexModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public IndexModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        public IList<Candidate> Candidate { get;set; } = default!;
        public IList<Party> Parties { get; set; }
        public IList<Position> Positions { get; set; }
        public IList<Campus> Colleges { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Candidate != null)
            {
                Parties = _context.Parties.ToList();
                Positions = _context.Positions.ToList();
                Colleges = _context.Colleges.Select(p => new Campus { Id = p.Id, Name = p.Name }).ToList();

                var candidates = await _context.Candidate.ToListAsync();

                Candidate = candidates.Select(x => new Models.Candidate
                {
                    Id = x.Id,
                    Name = x.Name,
                    CollegeId = x.CollegeId,
                    PartyId = x.PartyId,
                    PositionId = x.PositionId,
                    College = Colleges.FirstOrDefault(p => p.Id == x.CollegeId) == null
                        ? ""
                        : Colleges.First(p => p.Id == x.CollegeId).Name,
                    PartyName = Parties.FirstOrDefault(p => p.Id == x.PartyId) == null
                        ? ""
                        : Parties.First(p => p.Id == x.PartyId).DisplayName,
                    Position = Positions.FirstOrDefault(p => p.Id == x.PositionId) == null
                        ? ""
                        : Positions.First(p => p.Id == x.PositionId).DisplayName,
                    Symbol = x.Symbol
                }).ToList();

                //Candidate = await _context.Candidate.ToListAsync();
            }
        }
    }
}
