CREATE TABLE [dbo].[Messages] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Text]         NVARCHAR (1000)  NOT NULL,
    [IsRead]       BIT              NOT NULL,
    [IsAiAssisted] BIT              NOT NULL,
    [SenderId]     UNIQUEIDENTIFIER NULL,
    [GroupChatId]  UNIQUEIDENTIFIER NULL,
    [DialogChatId] UNIQUEIDENTIFIER NULL,
    [CreatedAt]    DATETIME2 (7)    NOT NULL,
    [UpdatedAt]    DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Messages_DialogChats_DialogChatId] FOREIGN KEY ([DialogChatId]) REFERENCES [dbo].[DialogChats] ([Id]),
    CONSTRAINT [FK_Messages_GroupChats_GroupChatId] FOREIGN KEY ([GroupChatId]) REFERENCES [dbo].[GroupChats] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_SenderId]
    ON [dbo].[Messages]([SenderId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_GroupChatId]
    ON [dbo].[Messages]([GroupChatId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Messages_DialogChatId]
    ON [dbo].[Messages]([DialogChatId] ASC);

