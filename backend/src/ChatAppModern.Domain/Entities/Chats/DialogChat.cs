namespace ChatAppModern.Domain.Entities.Chats;

public sealed class DialogChat : AuditableEntityBase
{
    public Guid? FirstUserId { get; set; }
    public Guid? SecondUserId { get; set; }
    
    public User? FirstUser { get; set; }
    public User? SecondUser { get; set; }
    public List<Message> Messages { get; set; } = new();
}