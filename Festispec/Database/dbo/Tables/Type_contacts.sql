CREATE TABLE [dbo].[Type_contacts] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [type] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Type_contact] PRIMARY KEY CLUSTERED ([id] ASC)
);

