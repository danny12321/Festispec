CREATE TABLE [dbo].[Inspections] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [description] NVARCHAR (500) NULL,
    [start_date]  DATETIME       NOT NULL,
    [end_date]    DATETIME       NOT NULL,
    [finished]    DATETIME       NULL,
    [festival_id] INT            NOT NULL,
    CONSTRAINT [PK_Inspections] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Inspections_Festivals] FOREIGN KEY ([festival_id]) REFERENCES [dbo].[Festivals] ([id])
);

