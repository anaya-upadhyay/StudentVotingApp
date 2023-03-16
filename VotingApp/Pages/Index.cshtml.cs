using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;

using VotingApp.Data;
using VotingApp.Models;
using VotingApp.Pages.PoliticalParty;

namespace VotingApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly VotingAppContext _context;

        public IndexModel(ILogger<IndexModel> logger,
                          VotingAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Summary Summary { get; set; } = default!;

        public void OnGet()
        {
            Summary = new();

            var candidateVotes = from candidate in _context.Candidate
                                  join vote in _context.Vote
                                  on candidate.Id equals vote.CandidateId
                                  select new
                                  {
                                      candidate = candidate.Id,
                                      partyId = candidate.PartyId,
                                      collegeId = candidate.CollegeId
                                  };

            var parties = _context.Parties;

            var partySummary = (from party in parties
                               select new PartySummary
                               {
                                   PartyName = party.DisplayName,
                                   NumberOfVotes = candidateVotes.Where(p => p.partyId == party.Id).Count()
                               }).ToList();

            if (partySummary.Any())
            {
                Summary.PartySummaries = partySummary;
            }
            else
            {
                // Demo Record
                Summary.PartySummaries = new List<PartySummary>
                {
                    new PartySummary{PartyName = "नेपाली काँग्रेस", NumberOfVotes = 10},
                    new PartySummary{PartyName = "नेपाल कम्युनिष्ट पार्टी (एमाले)", NumberOfVotes = 8},
                    new PartySummary{PartyName = "नेपाल कम्युनिष्ट पार्टी (माअाेवादी)", NumberOfVotes = 2},
                    new PartySummary{PartyName = "स्वतन्त्र", NumberOfVotes = 20}
                };
            }

            var colleges = _context.Colleges.ToList();

            var campusSummary = new List<CampusPartySummary>();

            foreach (var college in colleges)
            {
                partySummary = candidateVotes.Select(x => new PartySummary
                {
                    PartyName = parties.Where(p => p.Id == x.partyId).First().DisplayName,
                    NumberOfVotes = candidateVotes.Where(p => p.partyId == x.partyId && p.collegeId == college.Id).Count()
                }).ToList();

                if (partySummary.Any(p => p.NumberOfVotes > 0))
                    campusSummary.Add(new CampusPartySummary
                    {
                        CampusName = college.Name,
                        PartySummary = partySummary
                    });
            }

            #region The following query cannot work with Sqlite as such functionality is not supported
            
            //var campusSummary = (from college in _context.Colleges
            //                     select new CampusPartySummary
            //                     {
            //                         CampusName = college.Name,
            //                         PartySummary = candidateVotes.Select(x => new PartySummary
            //                         {
            //                             PartyName = parties.Where(p => p.Id == x.partyId).First().DisplayName,
            //                             NumberOfVotes = candidateVotes.Where(p => p.partyId == x.partyId && p.collegeId == college.Id).Count()
            //                         }).ToList()
            //                     }).ToList(); 

            #endregion

            if (campusSummary.Any())
            {
                Summary.CampusSummaries = campusSummary;
            }
            else
            {
                // Demo Record
                Summary.CampusSummaries = new List<CampusPartySummary>
                {
                    new CampusPartySummary{
                        CampusName = "सानाेठिमी क्याम्पस, सानाेठिमी",
                        PartySummary = Summary.PartySummaries
                    },
                    new CampusPartySummary{
                        CampusName = "महेन्द्र रत्न क्याम्पस, ताहाचल",
                        PartySummary = Summary.PartySummaries
                    }
                };
            }

            Summary.TotalCandidates = candidateVotes.Select(p => p.candidate).Distinct().Count();
            Summary.TotalVoters = _context.Users.Count();
            Summary.TotalVotes = _context.Votes.Count();
        }
    }
}