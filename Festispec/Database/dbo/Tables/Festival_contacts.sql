CREATE TABLE [dbo].[Festival_contacts](
	[Festival_id] [int] NOT NULL,
	[Contactperson_id] [int] NOT NULL,
 CONSTRAINT [PK_Festival_contacts] PRIMARY KEY CLUSTERED 
(
	[Festival_id] ASC,
	[Contactperson_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Festival_contacts]  WITH CHECK ADD  CONSTRAINT [FK_Festival_contacts_Contactpersons] FOREIGN KEY([Contactperson_id])
REFERENCES [dbo].[Contactpersons] ([id])
GO

ALTER TABLE [dbo].[Festival_contacts] CHECK CONSTRAINT [FK_Festival_contacts_Contactpersons]
GO
ALTER TABLE [dbo].[Festival_contacts]  WITH CHECK ADD  CONSTRAINT [FK_Festival_contacts_Festivals] FOREIGN KEY([Festival_id])
REFERENCES [dbo].[Festivals] ([id])
GO

ALTER TABLE [dbo].[Festival_contacts] CHECK CONSTRAINT [FK_Festival_contacts_Festivals]