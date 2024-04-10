namespace BlazorApp.Service;

public interface IService<TEntity> where TEntity : class {
  Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int predicate);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(int predicate);
        Task UpdateAsync(TEntity entity);
}