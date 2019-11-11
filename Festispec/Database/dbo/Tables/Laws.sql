CREATE TABLE [dbo].[Laws] (
    [id]              INT            IDENTITY (1, 1) NOT NULL,
    [municipality_id] INT            NOT NULL,
    [name]            NVARCHAR (50)  NOT NULL,
    [website]         NVARCHAR (200) NULL,
    [description]     NVARCHAR (500) NULL,
    CONSTRAINT [PK_Laws] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Laws_Municipalities] FOREIGN KEY ([municipality_id]) REFERENCES [dbo].[Municipalities] ([id])
);

