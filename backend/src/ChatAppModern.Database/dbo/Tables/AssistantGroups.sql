CREATE TABLE [dbo].[AssistantGroups] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (100)   NOT NULL,
    [Instructions] NVARCHAR (1000)  NOT NULL,
    [AssistantId]  NVARCHAR (200)   NOT NULL,
    [CreatorId]    UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_AssistantGroups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssistantGroups_AspNetUsers_CreatorId] FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AssistantGroups_CreatorId]
    ON [dbo].[AssistantGroups]([CreatorId] ASC);

