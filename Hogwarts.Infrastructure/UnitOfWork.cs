using Hogwarts.Domain.Repositories;
using System;

namespace Hogwarts.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly CandidateDbContext _context;

        public ICandidateRepository CandidateRepository { get; }
        
        public UnitOfWork(
            CandidateDbContext context,
            ICandidateRepository userRespository)
        {
            _context = context;
            CandidateRepository = userRespository;
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
