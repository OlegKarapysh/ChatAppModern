CREATE TABLE [dbo].[Connections] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [InitiatorId] INT            NULL,
    [InviteeId]   INT            NULL,
    [Status]      NVARCHAR (100) DEFAULT ('Pending') NOT NULL,
    [CreatedAt]   DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    [UpdatedAt]   DATETIME2 (7)  DEFAULT (getutcdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CHK_UsersDifferent] CHECK ([InitiatorId]<>[InviteeId]),
    FOREIGN KEY ([InitiatorId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    FOREIGN KEY ([InviteeId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

