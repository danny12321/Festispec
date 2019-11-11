CREATE TABLE [dbo].[Users] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [email]        NVARCHAR (100) NOT NULL,
    [password]     NVARCHAR (500) NOT NULL,
    [inspector_id] INT            NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Users_Inspectors] FOREIGN KEY ([inspector_id]) REFERENCES [dbo].[Inspectors] ([id])
);

