using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace VotingApp.Models;

public class Candidate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PartyId { get; set; }
    public int PositionId { get; set; }
    public int CollegeId { get; set; }
    [Required]
    [StringLength(10)]
    public string Symbol { get; set; }

    [NotMapped]
    [Display(Name = "Party")]
    public string? PartyName { get; set; }
    [NotMapped]
    [Display(Name = "Position")]
    public string? Position { get; set; }
    [NotMapped]
    [Display(Name = "College")]
    public string? College { get; set; }


    [NotMapped]
    public IEnumerable<SelectListItem> Parties { get; set; } = new List<SelectListItem>();
    [NotMapped]
    public IEnumerable<SelectListItem> Positions { get; set; } = new List<SelectListItem>();
    [NotMapped]
    public IEnumerable<SelectListItem> Colleges { get; set; } = new List<SelectListItem>();
}
