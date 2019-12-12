CREATE TABLE [dbo].[Clients](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[postalcode] [nvarchar](6) NULL,
	[country] [nvarchar](50) NULL,
 [city] NVARCHAR(50) NULL, 
	[street] [nvarchar](50) NULL,
	[housenumber] [nvarchar](50) NULL,
	[phone] [nvarchar](50) NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]