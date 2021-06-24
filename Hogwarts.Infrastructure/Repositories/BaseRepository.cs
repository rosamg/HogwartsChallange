using Hogwarts.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Hogwarts.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
         where TEntity : class
    {
        protected readonly CandidateDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        protected BaseRepository(CandidateDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return _context.Add(entity).Entity;
        }
        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
