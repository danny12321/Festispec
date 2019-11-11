CREATE TABLE [dbo].[Answers] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [answer]      NVARCHAR (200) NOT NULL,
    [question_id] INT            NULL,
    CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Answers_Questions] FOREIGN KEY ([question_id]) REFERENCES [dbo].[Questions] ([id])
);

