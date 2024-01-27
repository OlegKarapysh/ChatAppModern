namespace ChatAppModern.Domain.Repositories;

public interface IRepository<T> where T : EntityBase
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    IQueryable<T> SearchWhere<TSearch>(string? searchFilter);
    IQueryable<T> AsQueryable();
    IQueryable<T> ToSortedPage(string sortingProperty, SortingOrder sortingOrder, int page, int pageSize);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(Guid id);
    Task<bool> RemoveAsync(Guid id);
    Task<bool> RemoveRangeAsync(IEnumerable<Guid> entityIds);
}