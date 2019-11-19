CREATE TABLE [dbo].[Inspectors] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [lastname]    NVARCHAR (50) NOT NULL,
    [birthday]    DATETIME      NOT NULL,
    [postalcode]  NVARCHAR (6)  NOT NULL,
    [street]      NVARCHAR (50) NOT NULL,
    [housenumber] NVARCHAR (10) NOT NULL,
    [phone]       NVARCHAR (50) NULL,
    [active]      DATETIME      NULL,
    [latitude] NVARCHAR(50) NULL, 
    [longitude] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Inspectors] PRIMARY KEY CLUSTERED ([id] ASC)
);

