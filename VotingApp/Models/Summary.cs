using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models;

public class Summary
{
    [Display(Name = "जम्मा उम्मेदवार")]
    public int TotalCandidates { get; set; } = 10;
    [Display(Name = "जम्मा मतदाता")]
    public int TotalVoters { get; set; } = 20;
    [Display(Name = "संकलित भाेट")]
    public int TotalVotes { get; set; } = 200;
    [Display(Name = "पार्टी अनुसार संकलित मत")]
    public List<PartySummary> PartySummaries { get; set; }
    [Display(Name = "क्याम्पस अनुसार संकलित मत")]
    public List<CampusPartySummary> CampusSummaries { get; set; }
}

public class PartySummary
{
    public string PartyName { get; set; }
    public int NumberOfVotes { get; set; }
}

public class CampusPartySummary
{
    public string CampusName { get; set; }
    public List<PartySummary> PartySummary { get; set; }
}
