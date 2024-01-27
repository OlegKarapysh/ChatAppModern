namespace ChatAppModern.Domain.DTOs.Groups;

public class GroupInfoDto
{
    public Guid Id { get; set; }
    [MaxLength(AssistantGroup.MaxNameLength)]
    public string Name { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int MembersCount { get; set; }
    public int FilesCount { get; set; }
}