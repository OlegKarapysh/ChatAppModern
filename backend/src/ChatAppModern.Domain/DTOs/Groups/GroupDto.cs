namespace ChatAppModern.Domain.DTOs.Groups;

public class GroupDto : NewGroupDto
{
    public Guid Id { get; set; }
    public string AssistantId { get; set; } = string.Empty;
}