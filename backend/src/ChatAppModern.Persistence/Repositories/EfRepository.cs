namespace ChatAppModern.Persistence.Repositories;

public sealed class EfRepository<T> : IRepository<T> where T : EntityBase
{
    private readonly ChatDbContext _dbContext;

    public EfRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbContext.Set<T>().FindAsync(id);

    public async Task<List<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

    public IQueryable<T> SearchWhere<TSearch>(string? searchFilter)
        => _dbContext.Set<T>().SearchWhere<T, TSearch>(searchFilter);

    public IQueryable<T> ToSortedPage(string sortingProperty, SortingOrder sortingOrder, int page, int pageSize)
    {
        return _dbContext.Set<T>().OrderBy(sortingProperty, sortingOrder)
                         .Skip((page - 1) * pageSize)
                         .Take(pageSize);
    }

    public IQueryable<T> AsQueryable() => _dbContext.Set<T>();
    
    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        => await _dbContext.Set<T>().Where(predicate).ToListAsync();

    public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
        => await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);

    public async Task<T> AddAsync(T entity) => (await _dbContext.Set<T>().AddAsync(entity)).Entity;

    public ValueTask<T> Update(T entity) => ValueTask.FromResult(_dbContext.Set<T>().Update(entity).Entity);

    public async Task<bool> RemoveAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null)
        {
            return false;
        }
        
        _dbContext.Set<T>().Remove(entity);
        return true;
    }

    public async Task<bool> RemoveRangeAsync(IEnumerable<Guid> entityIds)
    {
        var hasUnsuccessfulDeletions = true;
        foreach (var id in entityIds)
        {
            if (!await RemoveAsync(id))
            {
                hasUnsuccessfulDeletions = false;
            }
        }

        return hasUnsuccessfulDeletions;
    }
}