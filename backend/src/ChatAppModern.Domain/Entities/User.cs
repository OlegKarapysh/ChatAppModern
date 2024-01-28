namespace ChatAppModern.Domain.Entities;

public class User : IdentityUser<Guid>, ICreatableEntity
{
    public const int MaxNameLength = 100;
    
    [MaxLength(MaxNameLength)]
    public string? FirstName { get; set; }
    [MaxLength(MaxNameLength)]
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime TokenExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<Message> Messages { get; set; } = new();
    public List<DialogChat> DialogChats { get; set; } = new();
    public List<GroupChat> GroupChats { get; set; } = new();
    public List<GroupChatMember> GroupChatMembers { get; set; } = new();
    public List<AssistantGroup> OwnGroups { get; set; } = new();
    public List<AssistantGroup> ForeignGroups { get; set; } = new();
    public List<AssistantGroupMember> GroupMembers { get; set; } = new();
}