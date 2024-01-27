namespace ChatAppModern.Domain.Entities.Chats;

public sealed class GroupChatMember : EntityBase
{
    public Guid? GroupChatId { get; set; }
    public Guid? MemberId { get; set; }
    public ChatMembershipType MembershipType { get; set; }

    public User? Member { get; set; }
    public GroupChat? GroupChat { get; set; }
}