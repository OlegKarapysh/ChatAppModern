namespace ChatAppModern.Domain.DTOs.Conversations;

public class DialogDto
{
    public Guid Id { get; set; }
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
}