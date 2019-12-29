CREATE TABLE [dbo].[Contactpersons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[lastname] [nvarchar](50) NOT NULL,
	[email] [nvarchar](100) NULL,
	[phone] [nvarchar](50) NULL,
	[client_id] [int] NULL,
	[type_contact] [int] NULL,
 CONSTRAINT [PK_Contactpersons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contactpersons]  WITH CHECK ADD  CONSTRAINT [FK_Contactpersons_Clients] FOREIGN KEY([client_id])
REFERENCES [dbo].[Clients] ([id])
GO

ALTER TABLE [dbo].[Contactpersons] CHECK CONSTRAINT [FK_Contactpersons_Clients]
GO
ALTER TABLE [dbo].[Contactpersons]  WITH CHECK ADD  CONSTRAINT [FK_Contactpersons_Type_contacts] FOREIGN KEY([type_contact])
REFERENCES [dbo].[Type_contacts] ([id])
GO

ALTER TABLE [dbo].[Contactpersons] CHECK CONSTRAINT [FK_Contactpersons_Type_contacts]