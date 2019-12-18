CREATE TABLE [dbo].[Possible_answer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[answer] [nvarchar](500) NOT NULL,
	[question_id] [int] NOT NULL,
 CONSTRAINT [PK_Possible_answer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Possible_answer]  WITH CHECK ADD  CONSTRAINT [FK_Possible_answer_Questions] FOREIGN KEY([question_id])
REFERENCES [dbo].[Questions] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Possible_answer] CHECK CONSTRAINT [FK_Possible_answer_Questions]