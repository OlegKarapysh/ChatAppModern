namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class AttachmentConfig : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Message)
               .WithMany(x => x.Attachments)
               .HasForeignKey(x => x.MessageId);
    }
}