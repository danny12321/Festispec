CREATE TABLE [dbo].[User_rolls](
	[user_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_User_rolls] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[User_rolls]  WITH CHECK ADD  CONSTRAINT [FK_User_rolls_Rolls] FOREIGN KEY([role_id])
REFERENCES [dbo].[Rolls] ([id])
GO

ALTER TABLE [dbo].[User_rolls] CHECK CONSTRAINT [FK_User_rolls_Rolls]
GO
ALTER TABLE [dbo].[User_rolls]  WITH CHECK ADD  CONSTRAINT [FK_User_rolls_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO

ALTER TABLE [dbo].[User_rolls] CHECK CONSTRAINT [FK_User_rolls_Users]