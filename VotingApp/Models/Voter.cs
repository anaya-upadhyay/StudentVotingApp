namespace VotingApp.Models;

public class Voter
{
    public int Id { get; set; }
    public int CollegeId { get; set; }
    public Semesters Semester { get; set; }
    public int RollNo { get; set; }
}
