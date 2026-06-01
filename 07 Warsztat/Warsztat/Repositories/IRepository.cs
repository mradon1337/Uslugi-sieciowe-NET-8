namespace BlogCMS.Repositories
{
    // Wspólny zestaw operacji CRUD dla dowolnego typu T
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
