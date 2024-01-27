namespace ChatAppModern.Domain.Entities.Chats;

public sealed class GroupChat : AuditableEntityBase
{
    public const int MaxTitleLength = 100;
    
    [MaxLength(MaxTitleLength)]
    public string Title { get; set; } = string.Empty;
    public List<Message> Messages { get; set; } = new();
    public List<User> Members { get; set; } = new();
    public List<GroupChatMember> ChatMembers { get; set; } = new();
}