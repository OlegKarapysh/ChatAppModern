namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class MessageConfig : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();
        
        builder.HasOne(x => x.GroupChat)
               .WithMany(x => x.Messages)
               .HasForeignKey(x => x.GroupChatId)
               .IsRequired(false);

        builder.HasOne(x => x.DialogChat)
               .WithMany(x => x.Messages)
               .HasForeignKey(x => x.DialogChatId)
               .IsRequired(false);
        
        builder.HasOne(x => x.Sender)
               .WithMany(x => x.Messages)
               .HasForeignKey(x => x.SenderId);
    }
}