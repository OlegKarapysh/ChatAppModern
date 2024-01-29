namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class GroupChatConfig : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();
        
        builder.HasMany(x => x.Members)
               .WithMany(x => x.GroupChats)
               .UsingEntity<GroupChatMember>(
                   l => l.HasOne(x => x.Member)
                         .WithMany(x => x.GroupChatMembers)
                         .HasForeignKey(x => x.MemberId),
                   r => r.HasOne(x => x.GroupChat)
                         .WithMany(x => x.ChatMembers)
                         .HasForeignKey(x => x.GroupChatId));
    }
}