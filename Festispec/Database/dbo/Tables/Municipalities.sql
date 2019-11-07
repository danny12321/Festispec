CREATE TABLE [dbo].[Municipalities] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Municipalities] PRIMARY KEY CLUSTERED ([id] ASC)
);

