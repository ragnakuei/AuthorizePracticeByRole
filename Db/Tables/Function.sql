CREATE TABLE [dbo].[Function]
(
    [Id]       INT            NOT NULL PRIMARY KEY IDENTITY,
    [Function] VARCHAR(100)   NOT NULL,
    [ParentId] INT            NULL,
    [Remark]   NVARCHAR(1000) NULL,
    [Created]  DATETIME2      NOT NULL
)

GO

ALTER TABLE [dbo].[Function]
    WITH CHECK ADD CONSTRAINT [FK_Function_ParentId] FOREIGN KEY ([ParentId])
        REFERENCES [dbo].[Function] ([Id])
GO

