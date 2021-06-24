namespace Hogwarts.Domain.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
