namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class AssistantGroupConfig : IEntityTypeConfiguration<AssistantGroup>
{
    public void Configure(EntityTypeBuilder<AssistantGroup> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Creator)
               .WithMany(x => x.OwnGroups)
               .HasForeignKey(x => x.CreatorId);
        
        builder.HasMany(x => x.Members)
               .WithMany(x => x.ForeignGroups)
               .UsingEntity<AssistantGroupMember>(
                   l => l.HasOne(x => x.User)
                         .WithMany(x => x.GroupMembers)
                         .HasForeignKey(x => x.UserId)
                         .OnDelete(DeleteBehavior.NoAction),
                   r => r.HasOne(x => x.Group)
                         .WithMany(x => x.GroupMembers)
                         .HasForeignKey(x => x.GroupId)
                         .OnDelete(DeleteBehavior.NoAction));

        builder.HasMany(x => x.Files)
               .WithOne(x => x.Group)
               .HasForeignKey(x => x.GroupId);
    }
}