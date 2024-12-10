CREATE TABLE [dbo].[PersonalMessages] (
    [Id]           INT             IDENTITY (1, 1) NOT NULL,
    [Text]         NVARCHAR (1000) NOT NULL,
    [IsRead]       BIT             NOT NULL,
    [SenderId]     INT             NULL,
    [ConnectionId] INT             NULL,
    [CreatedAt]    DATETIME2 (7)   DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]    DATETIME2 (7)   DEFAULT (getutcdate()) NOT NULL,
    CONSTRAINT [PK_PersonalMessages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PersonalMessages_AspNetUsers_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_PersonalMessages_Connections_ConnectionId] FOREIGN KEY ([ConnectionId]) REFERENCES [dbo].[Connections] ([Id])
);