using System.Collections.ObjectModel;

namespace VotingApp.Models;

public class Position
{
    public int Id { get; set; }
    public string SystemName { get; set; }
    public string DisplayName { get; set; }

    public Collection<Candidate> Candidates { get; set; }

}
