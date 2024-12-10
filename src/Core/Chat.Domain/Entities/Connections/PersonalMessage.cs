namespace Chat.Domain.Entities.Connections;

public sealed class PersonalMessage : AuditableEntityBase<int>
{
    public const int MaxTextLength = 1000;

    [MaxLength(MaxTextLength)]
    public string Text { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public int? SenderId { get; set; }
    public int? ConnectionId { get; set; }

    public User? Sender { get; set; }
    public Connection? Connection { get; set; }
}