namespace Chat.Domain.Entities.Groups;

public sealed class GroupMessage : AuditableEntityBase<int>
{
    public const int MaxTextLength = 1000;

    [MaxLength(MaxTextLength)]
    public string Text { get; set; } = string.Empty;
    public int? SenderId { get; set; }
    public int? GroupChatId { get; set; }

    public User? Sender { get; set; }
    public GroupChat? GroupChat { get; set; }
}