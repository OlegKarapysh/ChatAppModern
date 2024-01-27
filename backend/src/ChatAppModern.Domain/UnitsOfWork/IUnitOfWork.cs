namespace ChatAppModern.Domain.UnitsOfWork;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : EntityBase;
    Task SaveChangesAsync(CancellationToken token = default);
    Task RollBackChangesAsync(CancellationToken token = default);
}