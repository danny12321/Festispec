CREATE TABLE [dbo].[Questions] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [question]         NVARCHAR (500) NULL,
    [type_question]    INT            NOT NULL,
    [questionnaire_id] INT            NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Questions_Questionnaires] FOREIGN KEY ([questionnaire_id]) REFERENCES [dbo].[Questionnaires] ([id]),
    CONSTRAINT [FK_Questions_Type_questions] FOREIGN KEY ([type_question]) REFERENCES [dbo].[Type_questions] ([id])
);

