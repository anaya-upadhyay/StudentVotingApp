using System.Diagnostics;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using NuGet.Packaging;

using VotingApp.Data;
using VotingApp.Models;

namespace VotingApp.Pages.Votes
{
    public class CreateModel : PageModel
    {
        private readonly VotingApp.Data.VotingAppContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(VotingAppContext context,
                           SignInManager<IdentityUser> signInManager,
                           UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var signedInUser = _userManager.GetUserId(User);
            if (signedInUser == null)
            {
                return Page();
            }

            #region Move the following into a separate method - Commented now

            //var votes = _context.Vote.Where(p => p.VotedBy == signedInUser).Select(p => p.CandidateId).ToList();

            //var result = (from party in _context.Positions
            //              select new LayoutDto
            //              {
            //                  PositionId = party.Id,
            //                  PositionName = party.DisplayName,
            //                  Candidates = (from candidate in _context.Candidate.Where(p => p.PartyId == party.Id)
            //                                join vote in _context.Vote.Where(p => p.VotedBy == signedInUser)
            //                                on candidate.Id equals vote.CandidateId into voteLeft
            //                                from vote in voteLeft.DefaultIfEmpty()
            //                                select new CandidateDto
            //                                {
            //                                    Id = candidate.Id,
            //                                    Symbol = candidate.Symbol,
            //                                    IsVoted = vote != null && (vote.CandidateId == candidate.Id)
            //                                }).ToList()
            //              }).ToList();

            //VotingLayout.AddRange(result);

            //var myVotes = result.Select(p => new
            //{
            //    PositionId = p.PositionId.ToString(),
            //    CandidateSymbol = p.Candidates.FirstOrDefault(x => votes.Contains(x.Id)) == null
            //                    ? ""
            //                    : p.Candidates.First(x => votes.Contains(x.Id)).Symbol
            //}).ToList();

            //SelectedOptions = myVotes.ToDictionary(x => x.PositionId, x => x.CandidateSymbol);       

            #endregion

            await LoadVotingLayout(signedInUser);

            return Page();
        }

        //[BindProperty]
        public Vote Vote { get; set; } = default!;

        [BindProperty]
        public List<LayoutDto> VotingLayout { get; set; } = new();

        [BindProperty]
        public Dictionary<string, string> SelectedOptions { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            DateTime votedAt = DateTime.Now;

            //_context.Vote.RemoveRange(_context.Vote.ToList());
            //_context.SaveChanges();

            //return Page();

            var signedInUser = _userManager.GetUserId(User);
            if(signedInUser == null)
            {
                return Page();
            }

            var hasVoted = _context.Vote.Any(p => p.VotedBy == signedInUser);

            if (hasVoted)
            {
                ViewData["MessageColor"] = "red";
                ViewData["VotingMessage"] = "Your vote has already been submitted";

                await LoadVotingLayout(signedInUser);

                return Page();
            }

            foreach (var keyvaluepair in SelectedOptions)
            {
                if (keyvaluepair.Value != null)
                {
                    var myLayout = VotingLayout.FirstOrDefault(p => keyvaluepair.Key == p.PositionId.ToString());
                    if (myLayout != null)
                    {
                        var myCandi = myLayout.Candidates.FirstOrDefault(p => p.Symbol == keyvaluepair.Value);
                        if (myCandi != null)
                        {
                            Vote = new Vote
                            {
                                CandidateId = myCandi.Id,
                                VotedBy = signedInUser,
                                VotedAt = votedAt
                            };

                            _context.Vote.Add(Vote);
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            ViewData["MessageColor"] = "green";
            ViewData["VotingMessage"] = "Your vote has been submitted";

            await LoadVotingLayout(signedInUser);
            return Page();
            //return RedirectToPage("/Index");
        }

        private async Task LoadVotingLayout(string signedInUser)
        {
            VotingLayout.Clear();

            var votes = await _context.Vote.Where(p => p.VotedBy == signedInUser).Select(p => p.CandidateId).ToListAsync();

            var result = await (from party in _context.Positions
                          select new LayoutDto
                          {
                              PositionId = party.Id,
                              PositionName = party.DisplayName,
                              Candidates = (from candidate in _context.Candidate.Where(p => p.PartyId == party.Id)
                                            select new CandidateDto
                                            {
                                                Id = candidate.Id,
                                                Symbol = candidate.Symbol
                                            }).ToList()
                          }).ToListAsync();

            VotingLayout.AddRange(result);

            var myVotes = result.Select(p => new
            {
                PositionId = p.PositionId.ToString(),
                CandidateSymbol = p.Candidates.FirstOrDefault(x => votes.Contains(x.Id)) == null
                                ? ""
                                : p.Candidates.First(x => votes.Contains(x.Id)).Symbol
            }).ToList();

            SelectedOptions = myVotes.ToDictionary(x => x.PositionId, x => x.CandidateSymbol);
        }
    }
}
