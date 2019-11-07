CREATE TABLE [dbo].[Type_questions] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [type] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Type_questions] PRIMARY KEY CLUSTERED ([id] ASC)
);

