CREATE TABLE [dbo].[Questionnaires](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[version] [nvarchar](50) NULL,
	[inspector_id] [int] NULL,
	[inspection_id] [int] NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Questionnaires] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questionnaires]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaires_Inspectors_at_inspection] FOREIGN KEY([inspector_id], [inspection_id])
REFERENCES [dbo].[Inspectors_at_inspection] ([inpector_id], [inspection_id])
GO

ALTER TABLE [dbo].[Questionnaires] CHECK CONSTRAINT [FK_Questionnaires_Inspectors_at_inspection]