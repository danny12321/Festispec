CREATE TABLE [dbo].[Festivals] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [name]            NVARCHAR (50) NOT NULL,
    [postalcode]      NVARCHAR (6)  NULL,
    [city] NCHAR(50) NULL, 
    [street]          NVARCHAR (50) NULL,
    [housenumber]     NVARCHAR (10) NULL,
    [country]         NVARCHAR (50) NULL,
    [start_date]      DATETIME      NOT NULL,
    [end_date]        DATETIME      NOT NULL,
    [client_id]       INT           NOT NULL,
    [municipality_id] INT           NULL,
    [latitude]        NVARCHAR(50)    NULL,
    [longitude]       NVARCHAR(50)    NULL,
    CONSTRAINT [PK_Festivals] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Festivals_Clients] FOREIGN KEY ([client_id]) REFERENCES [dbo].[Clients] ([id]),
    CONSTRAINT [FK_Festivals_Municipalities] FOREIGN KEY ([municipality_id]) REFERENCES [dbo].[Municipalities] ([id])
);

