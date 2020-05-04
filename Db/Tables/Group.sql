CREATE TABLE [dbo].[Group]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Created] DATETIME2 NOT NULL
)

GO

CREATE INDEX [IX_Group_Name] ON [dbo].[Group] ([Name])
