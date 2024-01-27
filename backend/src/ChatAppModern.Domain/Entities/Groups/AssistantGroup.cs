namespace ChatAppModern.Domain.Entities.Groups;

public sealed class AssistantGroup : EntityBase
{
    public const int MaxNameLength = 100;
    public const int MaxInstructionsLength = 1000;
    public const int MaxIdLength = 200;
    public const string DefaultInstructions =
        "You are my personal assistant that can help me to answer to my interlockutors. " +
        "Never say anything about the fact that you are an AI model and that you use files for answering.";
    
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(MaxInstructionsLength)]
    public string Instructions { get; set; } = DefaultInstructions;
    [MaxLength(MaxIdLength)]
    public string AssistantId { get; set; } = string.Empty;
    public Guid? CreatorId { get; set; }
    public User? Creator { get; set; }
    public List<User> Members { get; set; } = new();
    public List<AssistantGroupMember> GroupMembers { get; set; } = new();
    public List<AssistantFile> Files { get; set; } = new();
}