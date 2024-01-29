﻿namespace ChatAppModern.Persistence.UnitsOfWork;

public sealed class EfUnitOfWork : IUnitOfWork
{
    private readonly ChatDbContext _dbContext;
    private readonly Dictionary<Type, object> _repositories = new();

    public EfUnitOfWork(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepository<T> GetRepository<T>() where T : EntityBase
    {
        var entityType = typeof(T);
        if (_repositories.TryGetValue(entityType, out var repository))
        {
            return (IRepository<T>)repository;
        }

        var repositoryType = typeof(EfRepository<T>);
        var newRepository = Activator.CreateInstance(repositoryType, _dbContext);
        if (newRepository is null)
        {
            throw new InvalidOperationException($"Cannot instantiate a repository of {repositoryType.FullName} type");
        }
        
        _repositories.Add(entityType, newRepository);

        return (IRepository<T>)newRepository;
    }

    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task RollBackChangesAsync(CancellationToken token = default)
    {
        await _dbContext.Database.RollbackTransactionAsync(token);
    }
}