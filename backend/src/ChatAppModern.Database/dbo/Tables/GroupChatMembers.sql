CREATE TABLE [dbo].[GroupChatMembers] (
    [GroupChatId]    UNIQUEIDENTIFIER NOT NULL,
    [MemberId]       UNIQUEIDENTIFIER NOT NULL,
    [MembershipType] INT              NOT NULL,
    CONSTRAINT [PK_GroupChatMembers] PRIMARY KEY CLUSTERED ([GroupChatId] ASC, [MemberId] ASC),
    CONSTRAINT [FK_GroupChatMembers_AspNetUsers_MemberId] FOREIGN KEY ([MemberId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GroupChatMembers_GroupChats_GroupChatId] FOREIGN KEY ([GroupChatId]) REFERENCES [dbo].[GroupChats] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_GroupChatMembers_MemberId]
    ON [dbo].[GroupChatMembers]([MemberId] ASC);

