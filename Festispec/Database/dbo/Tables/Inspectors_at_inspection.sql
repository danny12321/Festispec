CREATE TABLE [dbo].[Inspectors_at_inspection](
	[inpector_id] [int] NOT NULL,
	[inspection_id] [int] NOT NULL,
	[absent] [datetime] NULL,
 CONSTRAINT [PK_Inspectors_at_inspection] PRIMARY KEY CLUSTERED 
(
	[inpector_id] ASC,
	[inspection_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inspectors_at_inspection]  WITH CHECK ADD  CONSTRAINT [FK_Inspectors_at_inspection_Inspections] FOREIGN KEY([inspection_id])
REFERENCES [dbo].[Inspections] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Inspectors_at_inspection] CHECK CONSTRAINT [FK_Inspectors_at_inspection_Inspections]
GO
ALTER TABLE [dbo].[Inspectors_at_inspection]  WITH CHECK ADD  CONSTRAINT [FK_Inspectors_at_inspection_Inspectors] FOREIGN KEY([inpector_id])
REFERENCES [dbo].[Inspectors] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Inspectors_at_inspection] CHECK CONSTRAINT [FK_Inspectors_at_inspection_Inspectors]