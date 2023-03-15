using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using VotingApp.Data;
using VotingApp.Models;

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
            Summary.PartySummaries = new List<PartySummary>
            {
                new PartySummary{PartyName = "नेपाली काँग्रेस", NumberOfVotes = 10},
                new PartySummary{PartyName = "नेपाल कम्युनिष्ट पार्टी (एमाले)", NumberOfVotes = 8},
                new PartySummary{PartyName = "नेपाल कम्युनिष्ट पार्टी (माअाेवादी)", NumberOfVotes = 2},
                new PartySummary{PartyName = "स्वतन्त्र", NumberOfVotes = 20}
            };

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
    }
}