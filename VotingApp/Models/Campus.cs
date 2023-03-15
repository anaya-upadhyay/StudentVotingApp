using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models;

public class Campus
{
    public int Id { get; set; }
    public string Name { get; set; }
    [StringLength(100)]
    public string Address { get; set; }
    [Display(Name = "Phone Number")]
    [StringLength(50)]
    public string? PhoneNumber { get; set; }
    [StringLength(50)]
    public string? Email { get; set; }
    [StringLength(50)]
    public string? Website { get; set; }
}
