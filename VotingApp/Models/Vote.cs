using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models;

public class Vote
{
    public int Id { get; set; }
    public int CandidateId { get; set; }
    [StringLength(34)]
    public string VotedBy { get; set; }
    public DateTime VotedAt { get; set; }
}
