CREATE TABLE [dbo].[AssistantGroupMembers] (
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [GroupId]  UNIQUEIDENTIFIER NOT NULL,
    [ThreadId] NVARCHAR (200)   NULL,
    CONSTRAINT [PK_AssistantGroupMembers] PRIMARY KEY CLUSTERED ([GroupId] ASC, [UserId] ASC),
    CONSTRAINT [FK_AssistantGroupMembers_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AssistantGroupMembers_AssistantGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[AssistantGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AssistantGroupMembers_UserId]
    ON [dbo].[AssistantGroupMembers]([UserId] ASC);

