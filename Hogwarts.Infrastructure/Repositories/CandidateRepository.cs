using Hogwarts.Domain.Entities;
using Hogwarts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hogwarts.Infrastructure.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(CandidateDbContext context) : base(context)
        {
        }
        public async Task<Candidate> GetCandidateAsync(int candidateId)
        {
            var candidate = await _dbSet.FirstOrDefaultAsync(i => i.CandidateId == candidateId);

            return candidate;
        }

        public async Task<List<Candidate>> GetAllCandidatesAsync()
        {
            return await _context.Candidates.ToListAsync();
        }

        public Task<Candidate> FindByIdentifacationNumber(int identificationNumber)
        {
            var candidate = _dbSet.FirstOrDefaultAsync(c => c.IdentificationNumber == identificationNumber);
            return candidate;

        }
    }
}
