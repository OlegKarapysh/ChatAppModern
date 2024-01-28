namespace ChatAppModern.Persistence.Contexts;

public class ChatDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public const string SqlGetDateFunction = "getutcdate()";
    
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<DialogChat> DialogChats => Set<DialogChat>();
    public DbSet<GroupChat> GroupChats => Set<GroupChat>();
    public DbSet<GroupChatMember> GroupChatMembers => Set<GroupChatMember>();
    public DbSet<AssistantGroup> AssistantGroups => Set<AssistantGroup>();
    public DbSet<AssistantGroupMember> AssistantGroupMembers => Set<AssistantGroupMember>();
    public DbSet<AssistantFile> AssistantFiles => Set<AssistantFile>();
    
    public ChatDbContext()
    {
    }
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessageConfig).Assembly);
    }
}