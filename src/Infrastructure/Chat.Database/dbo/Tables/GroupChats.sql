CREATE TABLE [dbo].[GroupChats] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [GroupName] NVARCHAR (100) NOT NULL,
    [CreatorId] INT            NULL,
    [CreatedAt] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt] DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CreatorId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

