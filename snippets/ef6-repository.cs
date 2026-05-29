// EF6 Generic Repository with Caution
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id, CancellationToken ct);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
    Task AddAsync(T entity, CancellationToken ct);
    Task RemoveAsync(T entity, CancellationToken ct);
}

public sealed class EfRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    public EfRepository(DbContext context) => _context = context;

    public async Task<T> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Set<T>().FindAsync(ct, id);
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct)
    {
        return await _context.Set<T>()
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync(ct)
            .ConfigureAwait(false);
    }

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task RemoveAsync(T entity, CancellationToken ct)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
