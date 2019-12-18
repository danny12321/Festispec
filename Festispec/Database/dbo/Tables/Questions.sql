CREATE TABLE [dbo].[Questions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[question] [nvarchar](500) NULL,
	[type_question] [int] NOT NULL,
	[questionnaire_id] [int] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Questionnaires] FOREIGN KEY([questionnaire_id])
REFERENCES [dbo].[Questionnaires] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Questionnaires]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Type_questions] FOREIGN KEY([type_question])
REFERENCES [dbo].[Type_questions] ([id])
GO

ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Type_questions]