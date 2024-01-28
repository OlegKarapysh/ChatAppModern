namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class DialogChatConfig : IEntityTypeConfiguration<DialogChat>
{
    public void Configure(EntityTypeBuilder<DialogChat> builder)
    {
        builder.HasOne(x => x.FirstUser)
               .WithMany(x => x.DialogChats)
               .HasForeignKey(x => x.FirstUserId);
        
        builder.HasOne(x => x.SecondUser)
               .WithMany(x => x.DialogChats)
               .HasForeignKey(x => x.FirstUserId);
    }
}