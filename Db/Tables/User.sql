CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Account] NVARCHAR(50) NOT NULL, 
	[Password] NVARCHAR(100) NOT NULL, 	
    [Name] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME2 NOT NULL
)

GO

CREATE INDEX [IX_User_Account_Password] ON [dbo].[User] ([Account], [Password])
