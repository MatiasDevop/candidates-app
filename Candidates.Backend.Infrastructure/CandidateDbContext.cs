using Candidates.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Backend.Infrastructure
{
    public class CandidateDbContext :DbContext
    {
        public CandidateDbContext(DbContextOptions<CandidateDbContext> options) : base(options)
        {
        }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateExperience> CandidateExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>()
                .HasIndex(e => e.CandidateId)
                .IsUnique(true);

            modelBuilder.Entity<CandidateExperience>()
                .HasIndex(x => new { x.CandidateExperienceId, x.CandidateId })
                .IsUnique(true);
              
        }
    }
}
