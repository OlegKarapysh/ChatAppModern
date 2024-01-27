namespace ChatAppModern.Domain.Abstract;

public abstract class AuditableEntityBase : EntityBase, ICreatableEntity, IUpdatableEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}