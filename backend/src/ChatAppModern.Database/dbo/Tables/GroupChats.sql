CREATE TABLE [dbo].[GroupChats] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Title]     NVARCHAR (100)   NOT NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [UpdatedAt] DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_GroupChats] PRIMARY KEY CLUSTERED ([Id] ASC)
);

