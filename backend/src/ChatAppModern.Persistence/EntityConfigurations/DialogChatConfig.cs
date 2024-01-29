namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class DialogChatConfig : IEntityTypeConfiguration<DialogChat>
{
    public void Configure(EntityTypeBuilder<DialogChat> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.FirstUser)
               .WithMany(x => x.InitiatorDialogs)
               .HasForeignKey(x => x.FirstUserId);
        
        builder.HasOne(x => x.SecondUser)
               .WithMany(x => x.AcceptedDialogs)
               .HasForeignKey(x => x.SecondUserId);
    }
}