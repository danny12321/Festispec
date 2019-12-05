CREATE TABLE [dbo].[Inspections](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](500) NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[finished] [datetime] NULL,
	[festival_id] [int] NOT NULL,
 CONSTRAINT [PK_Inspections] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inspections]  WITH CHECK ADD  CONSTRAINT [FK_Inspections_Festivals] FOREIGN KEY([festival_id])
REFERENCES [dbo].[Festivals] ([id])
GO

ALTER TABLE [dbo].[Inspections] CHECK CONSTRAINT [FK_Inspections_Festivals]