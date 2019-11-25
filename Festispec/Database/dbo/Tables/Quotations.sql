CREATE TABLE [dbo].[Quotations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[price] [decimal](10, 2) NULL,
	[approved] [datetime] NOT NULL,
	[client_id] [int] NOT NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Quotations]  WITH CHECK ADD  CONSTRAINT [FK_Quotations_Clients] FOREIGN KEY([id])
REFERENCES [dbo].[Clients] ([id])
GO

ALTER TABLE [dbo].[Quotations] CHECK CONSTRAINT [FK_Quotations_Clients]