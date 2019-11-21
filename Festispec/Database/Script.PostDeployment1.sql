/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [dbo].[Rolls] ON 

INSERT [dbo].[Rolls] ([id], [role]) VALUES (1, N'Admin')
SET IDENTITY_INSERT [dbo].[Rolls] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id], [email], [password], [inspector_id]) VALUES (1, N'admin@admin.com', N'admin', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
INSERT [dbo].[User_rolls] ([user_id], [role_id]) VALUES (1, 1)