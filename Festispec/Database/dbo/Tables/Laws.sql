CREATE TABLE [dbo].[Laws](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[municipality_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[website] [nvarchar](200) NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_Laws] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Laws]  WITH CHECK ADD  CONSTRAINT [FK_Laws_Municipalities] FOREIGN KEY([municipality_id])
REFERENCES [dbo].[Municipalities] ([id])
GO

ALTER TABLE [dbo].[Laws] CHECK CONSTRAINT [FK_Laws_Municipalities]