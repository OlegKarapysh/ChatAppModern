namespace ChatAppModern.Persistence.EntityConfigurations;

public sealed class AssistantFileConfig : IEntityTypeConfiguration<AssistantFile>
{
    public void Configure(EntityTypeBuilder<AssistantFile> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql(ChatDbContext.SqlGetDateFunction)
               .ValueGeneratedOnUpdate();
    }
}