namespace ChatAppModern.Domain.DTOs.Groups;

public class GroupWithFilesDto : GroupDto
{
    public List<AssistantFileDto> Files { get; set; } = new();
}