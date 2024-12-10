namespace Chat.Persistence.EntityConfigurations;

public sealed class PersonalMessageConfig : IEntityTypeConfiguration<PersonalMessage>
{
    public void Configure(EntityTypeBuilder<PersonalMessage> builder)
    {
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();

        builder.HasOne(d => d.Connection)
               .WithMany(p => p.PersonalMessages)
               .HasForeignKey(d => d.ConnectionId);

        builder.HasOne(d => d.Sender)
               .WithMany(p => p.PersonalMessages)
               .HasForeignKey(d => d.SenderId);
    }
}