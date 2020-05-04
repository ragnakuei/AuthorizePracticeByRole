CREATE TABLE [dbo].[RoleFunctionAction]
(
    [RoleId]     INT       NOT NULL,
    [FunctionId] INT       NOT NULL,
    [ActionId]   INT       NULL,
    [Created]    DATETIME2 NOT NULL,
    PRIMARY KEY CLUSTERED
        (
         [RoleId] ASC,
         [FunctionId] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RoleFunctionAction]
    WITH CHECK ADD CONSTRAINT [FK_RoleFunctionAction_RoleId] FOREIGN KEY ([RoleId])
        REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[RoleFunctionAction]
    WITH CHECK ADD CONSTRAINT [FK_RoleFunctionAction_FunctionId] FOREIGN KEY ([FunctionId])
        REFERENCES [dbo].[Function] ([Id])
GO

ALTER TABLE [dbo].[RoleFunctionAction]
    WITH CHECK ADD CONSTRAINT [FK_RoleFunctionAction_ActionId] FOREIGN KEY ([ActionId])
        REFERENCES [dbo].[Action] ([Id])
GO