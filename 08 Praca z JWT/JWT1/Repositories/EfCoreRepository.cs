using Microsoft.EntityFrameworkCore;

namespace BlogCMS.Repositories
{
    // Repozytorium oparte na EF Core - działa dla każdego typu T
    public class EfCoreRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public EfCoreRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();

            // po zapisie EF sam uzupełnia Id, wyciągamy je przez refleksję
            var idProperty = typeof(T).GetProperty("Id");
            return (int)idProperty.GetValue(entity);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var id = (int)typeof(T).GetProperty("Id").GetValue(entity);

            // sprawdzamy czy taki rekord w ogóle istnieje
            var existing = await _entities.FindAsync(id);
            if (existing == null)
            {
                return false;
            }

            // podmieniamy stare wartości na nowe i zapisujemy
            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
