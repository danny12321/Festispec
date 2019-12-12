CREATE TABLE [dbo].[Answers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[answer] [nvarchar](200) NOT NULL,
	[question_id] [int] NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [FK_Answers_Questions] FOREIGN KEY([question_id])
REFERENCES [dbo].[Questions] ([id])
GO

ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [FK_Answers_Questions]