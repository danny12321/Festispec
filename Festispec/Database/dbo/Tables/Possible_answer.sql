CREATE TABLE [dbo].[Possible_answer] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [answer]      NVARCHAR (500) NOT NULL,
    [question_id] INT            NOT NULL,
    CONSTRAINT [PK_Possible_answer] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Possible_answer_Questions] FOREIGN KEY ([question_id]) REFERENCES [dbo].[Questions] ([id])
);

