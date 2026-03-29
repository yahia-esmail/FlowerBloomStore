

namespace FlowerBloomStore.Infrastructure.Repositories.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbset;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) 
            => await _dbset.AddAsync(entity);

        public void Delete(TEntity entity)
            => _dbset.Remove(entity);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbset.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _dbset.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _dbset.FindAsync(id);

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Update(TEntity entity)
            => _dbset.Update(entity);

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            if (predicate == null)
                return await _dbset.CountAsync();

            return await _dbset.CountAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbset.AnyAsync(predicate);
        }
    }
}
