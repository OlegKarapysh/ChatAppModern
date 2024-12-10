CREATE TABLE [dbo].[GroupMembers] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [GroupChatId] INT           NULL,
    [UserId]      INT           NULL,
    [CreatedAt]   DATETIME2 (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]   DATETIME2 (7) DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GroupChatId]) REFERENCES [dbo].[GroupChats] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);