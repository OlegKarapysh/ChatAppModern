namespace Chat.Persistence.Contexts;

public class ChatDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public const string SqlGetDateFunction = "getutcdate()";

    public DbSet<Connection> Connections => Set<Connection>();
    public DbSet<PersonalMessage> PersonalMessages => Set<PersonalMessage>();
    public DbSet<GroupChat> GroupChats => Set<GroupChat>();
    public DbSet<GroupMember> GroupMembers => Set<GroupMember>();
    public DbSet<GroupMessage> GroupMessages => Set<GroupMessage>();

    public ChatDbContext()
    {
    }
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonalMessageConfig).Assembly);
    }
}