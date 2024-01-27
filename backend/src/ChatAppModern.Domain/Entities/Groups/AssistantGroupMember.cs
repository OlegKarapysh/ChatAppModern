namespace ChatAppModern.Domain.Entities.Groups;

public sealed class AssistantGroupMember : EntityBase
{
    public const int MaxIdLength = 200;
    
    public Guid? UserId { get; set; }
    public Guid? GroupId { get; set; }
    [MaxLength(MaxIdLength)]
    public string? ThreadId { get; set; }
    public User? User { get; set; }
    public AssistantGroup? Group { get; set; }
}