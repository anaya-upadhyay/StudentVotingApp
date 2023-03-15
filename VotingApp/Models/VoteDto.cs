using Microsoft.Build.Framework;

namespace VotingApp.Models;

public class VoteDto
{
    public int CollegeId { get; set; } = 0;
    public int RollNo { get; set; }
    public Semesters Semester { get; set; }
    public List<int> Candidates { get; set; } = new();
}

public class LayoutDto
{
    public int PositionId { get; set; }
    public string PositionName { get; set; }
    public List<CandidateDto> Candidates { get; set; }
}

public class CandidateDto
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public bool? IsVoted { get; set; }
}