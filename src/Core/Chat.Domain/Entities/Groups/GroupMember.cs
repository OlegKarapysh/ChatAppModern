namespace Chat.Domain.Entities.Groups;

public sealed class GroupMember : AuditableEntityBase<int>
{
    public int? UserId { get; set; }
    public int? GroupChatId { get; set; }

    public User? User { get; set; }
    public GroupChat? GroupChat { get; set; }
}