namespace ChatAppModern.Domain.Entities.Attachments;

public sealed class Attachment : EntityBase
{
    public const int MaxSourceUrlLength = 4000;
    
    [MaxLength(MaxSourceUrlLength)]
    public string SourceUrl { get; set; } = string.Empty;
    public AttachmentType Type { get; set; } = AttachmentType.File;
    public int MessageId { get; set; }
    
    public Message? Message { get; set; }
}