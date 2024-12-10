namespace Chat.Domain.Entities;

public class User : IdentityUser<int>, ICreatableEntity, IEntity<int>
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

    public List<Connection> InitiatedConnections { get; set; } = new();
    public List<Connection> ReceivedConnections { get; set; } = new();
    public List<GroupChat> OwnGroups { get; set; } = new();
    public List<GroupChat> ForeignGroups { get; set; } = new();
    public List<GroupMember> GroupMembers { get; set; } = new();
    public List<PersonalMessage> PersonalMessages { get; set; } = new();
    public List<GroupMessage> GroupMessages { get; set; }
}