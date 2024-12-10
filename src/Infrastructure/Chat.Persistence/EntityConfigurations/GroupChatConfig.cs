namespace Chat.Persistence.EntityConfigurations;

public sealed class GroupChatConfig : IEntityTypeConfiguration<GroupChat>
{
    public void Configure(EntityTypeBuilder<GroupChat> builder)
    {
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();

        builder.HasOne(x => x.Creator)
               .WithMany(x => x.OwnGroups)
               .HasForeignKey(x => x.CreatorId);

        builder.HasMany(x => x.Members)
               .WithMany(x => x.ForeignGroups)
               .UsingEntity<GroupMember>(
                   l => l.HasOne(x => x.User)
                         .WithMany(x => x.GroupMembers)
                         .HasForeignKey(x => x.UserId)
                         .OnDelete(DeleteBehavior.NoAction),
                   r => r.HasOne(x => x.GroupChat)
                         .WithMany(x => x.GroupMembers)
                         .HasForeignKey(x => x.GroupChatId)
                         .OnDelete(DeleteBehavior.NoAction));
    }
}