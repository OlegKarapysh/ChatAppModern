namespace ChatAppModern.Domain.Entities;

public sealed class Message : AuditableEntityBase
{
    public const int MaxTextLength = 1000;
    
    [MaxLength(MaxTextLength)]
    public string Text { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public bool IsAiAssisted { get; set; }
    public Guid? SenderId { get; set; }
    public Guid? GroupChatId { get; set; }
    public Guid? DialogChatId { get; set; }
    
    public User? Sender { get; set; }
    public GroupChat? GroupChat { get; set; }
    public DialogChat? DialogChat { get; set; }
    public List<Attachment> Attachments { get; set; } = new();
}