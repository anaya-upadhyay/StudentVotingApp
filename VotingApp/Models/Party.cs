using System.Collections.ObjectModel;

namespace VotingApp.Models;

public class Party
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string DisplayName { get; set; }

    public Collection<Candidate> Candidates { get; set; }
}
