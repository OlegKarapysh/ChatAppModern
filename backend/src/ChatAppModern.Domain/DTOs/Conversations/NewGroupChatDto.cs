namespace ChatAppModern.Domain.DTOs.Conversations;

public class NewGroupChatDto
{
    public Guid CreatorId { get; set; }
    public string Title { get; set; } = string.Empty;
}