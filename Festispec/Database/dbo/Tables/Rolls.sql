CREATE TABLE [dbo].[Rolls] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [role] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Rolls] PRIMARY KEY CLUSTERED ([id] ASC)
);

