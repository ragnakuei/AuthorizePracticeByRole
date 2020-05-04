CREATE TABLE [dbo].[Action]
(
    [Id]         INT            NOT NULL PRIMARY KEY IDENTITY,
    [Action]     VARCHAR(100)   NOT NULL,
    [FunctionId] INT            NOT NULL,
    [Remark]     NVARCHAR(1000) NOT NULL,
    [Created]    DATETIME2      NOT NULL
)
GO

CREATE INDEX [IX_Action_Id_FunctionId] ON [dbo].[Action] ([FunctionId], [Id])
GO

ALTER TABLE [dbo].[Action]
    WITH CHECK ADD CONSTRAINT [FK_Action_FunctionId] FOREIGN KEY ([FunctionId])
        REFERENCES [dbo].[Function] ([Id])
GO

