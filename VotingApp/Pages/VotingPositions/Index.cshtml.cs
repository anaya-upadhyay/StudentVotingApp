using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.VotingPositions
{
    public class IndexModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public IndexModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        public IList<Position> Position { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Positions != null)
            {
                Position = await _context.Positions.ToListAsync();
            }
        }
    }
}
