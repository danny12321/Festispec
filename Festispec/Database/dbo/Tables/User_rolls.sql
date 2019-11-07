CREATE TABLE [dbo].[User_rolls] (
    [user_id] INT NOT NULL,
    [role_id] INT NOT NULL,
    CONSTRAINT [PK_User_rolls] PRIMARY KEY CLUSTERED ([user_id] ASC, [role_id] ASC),
    CONSTRAINT [FK_User_rolls_Rolls] FOREIGN KEY ([role_id]) REFERENCES [dbo].[Rolls] ([id]),
    CONSTRAINT [FK_User_rolls_Users] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id])
);

