namespace FlowerBloomStore.Application.Services
{
    public class GenericService<T> : IService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        public GenericService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _repository.SaveChangesAsync();
            }
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await _repository.CountAsync(predicate);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.AnyAsync(predicate);
        }

        public virtual async Task SaveChangesAsync()
        {
             await _repository.SaveChangesAsync();
        }
    }
}