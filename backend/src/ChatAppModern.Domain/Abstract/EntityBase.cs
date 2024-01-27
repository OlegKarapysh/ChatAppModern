namespace ChatAppModern.Domain.Abstract;

public abstract class EntityBase
{
    public Guid Id { get; } = Guid.NewGuid();
}