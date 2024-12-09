﻿namespace Chat.Domain.Entities;

public class Message : AuditableEntityBase<int>
{
    public const int MaxTextLength = 1000;
    
    [MaxLength(MaxTextLength)]
    public string TextContent { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public bool IsAiAssisted { get; set; }
    public int? SenderId { get; set; }
    public int? ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
    public User? Sender { get; set; }
    public IList<Attachment> Attachments { get; set; } = new List<Attachment>();
}