namespace ChatAppModern.Domain.Entities;

public sealed class AssistantFile : AuditableEntityBase
{
    public const int MaxFileNameLength = 100;
    public const int MaxIdLength = 200;
    
    [MaxLength(MaxFileNameLength)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(MaxIdLength)]
    public string FileId { get; set; } = string.Empty;
    public int SizeInBytes { get; set; }
    
    public Guid? GroupId { get; set; }
    public AssistantGroup? Group { get; set; }
}