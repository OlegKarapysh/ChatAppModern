namespace Chat.Domain.Entities.Connections;

public sealed class Connection : AuditableEntityBase<int>
{
    public ConnectionStatus Status { get; set; }
    public int? InitiatorId { get; set; }
    public int? InviteeId { get; set; }

    public User? Initiator { get; set; }
    public User? Invitee { get; set; }
    public List<PersonalMessage> PersonalMessages { get; set; } = new();
}