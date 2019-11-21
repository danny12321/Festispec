CREATE TABLE [dbo].[Contactpersons] (
    [id]           INT            IDENTITY (1, 1) NOT NULL,
    [name]         NVARCHAR (50)  NOT NULL,
    [lastname]     NVARCHAR (50)  NOT NULL,
    [email]        NVARCHAR (100) NOT NULL,
    [phone]        NVARCHAR (50)  NOT NULL,
    [festival_id]  INT            NULL,
    [client_id]    INT            NULL,
    [type_contact] INT            NOT NULL,
    CONSTRAINT [PK_Contactpersons] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Contactpersons_Clients] FOREIGN KEY ([id]) REFERENCES [dbo].[Clients] ([id]),
    CONSTRAINT [FK_Contactpersons_Festivals] FOREIGN KEY ([festival_id]) REFERENCES [dbo].[Festivals] ([id]),
    CONSTRAINT [FK_Contactpersons_Type_contacts] FOREIGN KEY ([type_contact]) REFERENCES [dbo].[Type_contacts] ([id])
);

