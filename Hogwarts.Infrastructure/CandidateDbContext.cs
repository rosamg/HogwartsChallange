using Hogwarts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Infrastructure
{
    public class CandidateDbContext : DbContext
    {
        public CandidateDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Candidate> Candidates { get; set; }
    }
}
