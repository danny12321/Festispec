CREATE TABLE [dbo].[Inspectors_availability] (
    [id]           INT      IDENTITY (1, 1) NOT NULL,
    [start_date]   DATETIME NOT NULL,
    [end_date]     DATETIME NOT NULL,
    [inspector_id] INT      NOT NULL,
    CONSTRAINT [PK_Inspectors_availability] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Inspectors_availability_Inspectors] FOREIGN KEY ([inspector_id]) REFERENCES [dbo].[Inspectors] ([id]) ON DELETE CASCADE ON UPDATE CASCADE
);

