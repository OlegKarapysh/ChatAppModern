namespace Chat.Persistence.EntityConfigurations;

public sealed class GroupMessageConfig : IEntityTypeConfiguration<GroupMessage>
{
    public void Configure(EntityTypeBuilder<GroupMessage> builder)
    {
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();

        builder.HasOne(x => x.GroupChat)
               .WithMany(x => x.Messages)
               .HasForeignKey(x => x.GroupChatId);

        builder.HasOne(x => x.Sender)
               .WithMany(x => x.GroupMessages)
               .HasForeignKey(x => x.SenderId);
    }
}