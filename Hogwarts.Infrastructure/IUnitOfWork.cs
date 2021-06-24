using Hogwarts.Domain.Repositories;

namespace Hogwarts.Infrastructure
{
    public interface IUnitOfWork
    {
        ICandidateRepository CandidateRepository { get; }
        int Commit();
    }
}
