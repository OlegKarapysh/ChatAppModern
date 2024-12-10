CREATE TABLE [dbo].[GroupMessages] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [GroupChatId] INT            NULL,
    [SenderId]    INT            NULL,
    [Text]        NVARCHAR (MAX) NOT NULL,
    [CreatedAt]   DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]   DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([GroupChatId]) REFERENCES [dbo].[GroupChats] ([Id]),
    FOREIGN KEY ([SenderId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

