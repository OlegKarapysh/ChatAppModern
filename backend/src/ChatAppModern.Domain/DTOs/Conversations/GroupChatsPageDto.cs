namespace ChatAppModern.Domain.DTOs.Conversations;

public class GroupChatsPageDto : PageDto
{
    public GroupChatBasicInfoDto[]? GroupChats { get; set; }
}