CREATE TABLE [dbo].[Questionnaires] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [version]       NVARCHAR (50) NULL,
    [inspector_id]  INT           NOT NULL,
    [inspection_id] INT           NOT NULL,
    CONSTRAINT [PK_Questionnaires] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Questionnaires_Inspectors_at_inspection] FOREIGN KEY ([inspector_id], [inspection_id]) REFERENCES [dbo].[Inspectors_at_inspection] ([inpector_id], [inspection_id])
);

