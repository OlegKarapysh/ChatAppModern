namespace Chat.Persistence.EntityConfigurations;

public sealed class ConnectionConfig : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.Property(c => c.Status)
               .HasConversion<string>()
               .HasMaxLength(100);

        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();

        builder.HasOne(x => x.Initiator)
               .WithMany(x => x.InitiatedConnections)
               .HasForeignKey(x => x.InitiatorId);

        builder.HasOne(x => x.Invitee)
               .WithMany(x => x.ReceivedConnections)
               .HasForeignKey(x => x.InviteeId);

        builder.HasMany(x => x.PersonalMessages)
               .WithOne(x => x.Connection)
               .HasForeignKey(x => x.ConnectionId);
    }
}