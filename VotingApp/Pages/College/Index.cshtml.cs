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
    public class IndexModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;

        public IndexModel(VotingApp.Data.VotingAppContext context)
        {
            _context = context;
        }

        public IList<Campus> Campus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Colleges != null)
            {
                Campus = await _context.Colleges.ToListAsync();
            }
        }
    }
}
