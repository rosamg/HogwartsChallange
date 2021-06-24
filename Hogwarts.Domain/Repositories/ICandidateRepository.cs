using Hogwarts.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hogwarts.Domain.Repositories
{
    public interface ICandidateRepository : IBaseRepository<Candidate>
    {
        Task<Candidate> GetCandidateAsync(int candidateId);
        Task<List<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> FindByIdentifacationNumber(int identificationNumber);

    }
}
