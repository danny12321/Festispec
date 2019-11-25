CREATE TABLE [dbo].[Festivals](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[postalcode] [nvarchar](6) NULL,
	[city] [nvarchar](50) NULL,
	[street] [nvarchar](50) NULL,
	[housenumber] [nvarchar](10) NULL,
	[country] [nvarchar](50) NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[client_id] [int] NOT NULL,
	[municipality_id] [int] NULL,
	[latitude] [nvarchar](50) NULL,
	[longitude] [nvarchar](50) NULL,
 CONSTRAINT [PK_Festivals] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Festivals]  WITH CHECK ADD  CONSTRAINT [FK_Festivals_Clients] FOREIGN KEY([client_id])
REFERENCES [dbo].[Clients] ([id])
GO

ALTER TABLE [dbo].[Festivals] CHECK CONSTRAINT [FK_Festivals_Clients]
GO
ALTER TABLE [dbo].[Festivals]  WITH CHECK ADD  CONSTRAINT [FK_Festivals_Municipalities] FOREIGN KEY([municipality_id])
REFERENCES [dbo].[Municipalities] ([id])
GO

ALTER TABLE [dbo].[Festivals] CHECK CONSTRAINT [FK_Festivals_Municipalities]