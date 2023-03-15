﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.College
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
            return Page();
        }

        [BindProperty]
        public Campus Campus { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Colleges == null || Campus == null)
            {
                return Page();
            }

            _context.Colleges.Add(Campus);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
