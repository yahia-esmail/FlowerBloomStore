

namespace FlowerBloomStore.Domain.Interfaces.GenericRepo
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
