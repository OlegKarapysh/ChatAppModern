CREATE TABLE [dbo].[DialogChats] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [FirstUserId]  UNIQUEIDENTIFIER NULL,
    [SecondUserId] UNIQUEIDENTIFIER NULL,
    [CreatedAt]    DATETIME2 (7)    NOT NULL,
    [UpdatedAt]    DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_DialogChats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DialogChats_AspNetUsers_FirstUserId] FOREIGN KEY ([FirstUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_DialogChats_AspNetUsers_SecondUserId] FOREIGN KEY ([SecondUserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_DialogChats_SecondUserId]
    ON [dbo].[DialogChats]([SecondUserId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_DialogChats_FirstUserId]
    ON [dbo].[DialogChats]([FirstUserId] ASC);

