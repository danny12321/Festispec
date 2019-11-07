CREATE TABLE [dbo].[Quotations] (
    [id]        INT             IDENTITY (1, 1) NOT NULL,
    [price]     DECIMAL (10, 2) NULL,
    [approved]  DATETIME        NOT NULL,
    [client_id] INT             NOT NULL,
    CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Quotations_Clients] FOREIGN KEY ([id]) REFERENCES [dbo].[Clients] ([id])
);

