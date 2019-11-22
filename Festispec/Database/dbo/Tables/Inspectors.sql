CREATE TABLE [dbo].[Inspectors] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [lastname]    NVARCHAR (50) NOT NULL,
    [email] NVARCHAR(50) NULL, 
    [birthday]    DATETIME      NULL,
    [postalcode]  NVARCHAR (6)  NULL,
    [country] NVARCHAR(50) NULL, 
    [city] NVARCHAR(50) NULL, 
    [street]      NVARCHAR (50) NULL,
    [housenumber] NVARCHAR (10) NULL,
    [phone]       NVARCHAR (50) NULL,
    [active]      DATETIME      NULL,
    [latitude] NVARCHAR(50) NULL, 
    [longitude] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Inspectors] PRIMARY KEY CLUSTERED ([id] ASC)
);

