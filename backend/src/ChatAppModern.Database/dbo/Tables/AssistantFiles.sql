CREATE TABLE [dbo].[AssistantFiles] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (100)   NOT NULL,
    [FileId]      NVARCHAR (200)   NOT NULL,
    [SizeInBytes] INT              NOT NULL,
    [GroupId]     UNIQUEIDENTIFIER NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [UpdatedAt]   DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_AssistantFiles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AssistantFiles_AssistantGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [dbo].[AssistantGroups] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_AssistantFiles_GroupId]
    ON [dbo].[AssistantFiles]([GroupId] ASC);

