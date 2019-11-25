CREATE TABLE [dbo].[Inspectors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[lastname] [nvarchar](50) NOT NULL,
	[birthday] [datetime] NOT NULL,
	[postalcode] [nvarchar](6) NOT NULL,
	[street] [nvarchar](50) NOT NULL,
	[housenumber] [nvarchar](10) NOT NULL,
	[phone] [nvarchar](50) NULL,
	[active] [datetime] NULL,
	[latitude] [nvarchar](50) NULL,
	[longitude] [nvarchar](50) NULL,
 CONSTRAINT [PK_Inspectors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]