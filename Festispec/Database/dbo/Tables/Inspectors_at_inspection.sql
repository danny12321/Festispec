CREATE TABLE [dbo].[Inspectors_at_inspection] (
    [inpector_id]   INT      NOT NULL,
    [inspection_id] INT      NOT NULL,
    [absent]        DATETIME NOT NULL,
    CONSTRAINT [PK_Inspectors_at_inspection] PRIMARY KEY CLUSTERED ([inpector_id] ASC, [inspection_id] ASC),
    CONSTRAINT [FK_Inspectors_at_inspection_Inspections] FOREIGN KEY ([inspection_id]) REFERENCES [dbo].[Inspections] ([id]),
    CONSTRAINT [FK_Inspectors_at_inspection_Inspectors] FOREIGN KEY ([inpector_id]) REFERENCES [dbo].[Inspectors] ([id])
);

