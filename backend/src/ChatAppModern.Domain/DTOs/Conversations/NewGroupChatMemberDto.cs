namespace ChatAppModern.Domain.DTOs.Conversations;

public class NewGroupChatMemberDto
{
    public Guid GroupChatId { get; set; }
    public string MemberUserName { get; set; } = string.Empty;
}