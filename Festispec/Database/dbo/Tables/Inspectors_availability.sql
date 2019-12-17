CREATE TABLE [dbo].[Inspectors_availability](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[inspector_id] [int] NOT NULL,
 [text] NVARCHAR(256) NULL, 
    CONSTRAINT [PK_Inspectors_availability] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inspectors_availability]  WITH CHECK ADD  CONSTRAINT [FK_Inspectors_availability_Inspectors] FOREIGN KEY([inspector_id])
REFERENCES [dbo].[Inspectors] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Inspectors_availability] CHECK CONSTRAINT [FK_Inspectors_availability_Inspectors]