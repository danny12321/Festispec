CREATE TABLE [dbo].[Clients] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [postalcode]  NVARCHAR (6)  NULL,
    [city] NVARCHAR(50) NULL, 
    [street]      NVARCHAR (50) NULL,
    [housenumber] NVARCHAR (50) NULL,
    [country]     NVARCHAR (50) NULL,
    [phone]       NVARCHAR (50) NULL,
    CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([id] ASC)
);

