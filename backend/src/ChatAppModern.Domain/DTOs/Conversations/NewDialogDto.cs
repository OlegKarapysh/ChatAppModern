namespace ChatAppModern.Domain.DTOs.Conversations;

public class NewDialogDto
{
    public Guid CreatorId { get; set; }
    public string InterlocutorUserName { get; set; } = string.Empty;
}