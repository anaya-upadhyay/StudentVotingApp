using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using VotingApp.Models;

namespace VotingApp.Data;

public class VotingAppContext : IdentityDbContext<IdentityUser>
{
    public VotingAppContext(DbContextOptions<VotingAppContext> options)
        : base(options)
    {
    }

    public DbSet<Campus> Colleges { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<Position> Positions { get; set; }    
    public DbSet<Candidate> Candidate { get; set; }
    public DbSet<Voter> Voters { get; set; }
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Campus>(b =>
        {
            b.Property(p => p.PhoneNumber).IsRequired(false);
            b.Property(p => p.Email).IsRequired(false);
            b.Property(p => p.Website).IsRequired(false);
        });
    }

    public DbSet<VotingApp.Models.Vote> Vote { get; set; } = default!;

}
