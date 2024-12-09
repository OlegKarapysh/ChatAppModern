﻿namespace Chat.Persistence.Contexts;

public class ChatDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public const string SqlGetDateFunction = "getutcdate()";
    
    public DbSet<Conversation> Conversations => Set<Conversation>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<ConversationParticipant> ConversationParticipants => Set<ConversationParticipant>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<GroupMember> GroupMembers => Set<GroupMember>();
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