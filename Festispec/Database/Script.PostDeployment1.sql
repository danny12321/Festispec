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

INSERT [dbo].[Rolls] ([role]) VALUES (N'Admin')
INSERT [dbo].[Rolls] ([role]) VALUES (N'Inspector')
INSERT [dbo].[Users] ([email], [password], [inspector_id]) VALUES (N'admin@admin.com', N'admin', NULL)
INSERT [dbo].[User_rolls] ([user_id], [role_id]) VALUES (1, 1)
INSERT [dbo].[Municipalities] ([name]) VALUES (N'Utrecht')
INSERT [dbo].[Clients] ([name], [postalcode], [street], [housenumber], [country], [phone]) VALUES (N'Qbase', N'7890XX', N'Straatweg', N'13', N'Nederland', N'3152332567')
INSERT [dbo].[Festivals] ([name], [postalcode],[street], [housenumber], [country], [start_date], [end_date], [client_id], [municipality_id], [latitude], [longitude], [city]) VALUES (N'Lowlands', N'1234AA', N'Kerkweg', N'55', N'Nederland', '20200610 12:00:00', '20200611 20:00:00', 1, 1, N'52.4371889', N'5.7548392', N'Biddinghuizen')
INSERT [dbo].[Inspectors] ([name], [lastname], [birthday], [country], [city], [postalcode], [street], [housenumber], [phone], [active], [latitude], [longitude]) VALUES (N'Peter', N'de Gekke', '19800420 00:00:00', N'Nederland', N'Amsterdam', N'1234AA', N'Laanweg', N'20', N'0611092837', '20191121 13:00:00', N'52.3736398', N'4.8990725')
INSERT [dbo].[Type_questions] ([type]) VALUES (N'Open')
INSERT [dbo].[Type_questions] ([type]) VALUES (N'Multiple Choise')
INSERT [dbo].[Type_questions] ([type]) VALUES (N'Select')
INSERT [dbo].[Type_contacts] ([type]) VALUES (N'Administratief')
INSERT [dbo].[Type_contacts] ([type]) VALUES (N'Leidinggevend')
INSERT [dbo].[Type_contacts] ([type]) VALUES (N'Commercieel')
INSERT [dbo].[Type_contacts] ([type]) VALUES (N'Economisch')