IF NOT EXISTS(SELECT *
              FROM [dbo].[User])
    BEGIN
        ALTER TABLE [dbo].[GroupRole]
            DROP CONSTRAINT [FK_GroupRole_GroupId]
        ALTER TABLE [dbo].[GroupRole]
            DROP CONSTRAINT [FK_GroupRole_RoleId]

        ALTER TABLE [dbo].[UserGroup]
            DROP CONSTRAINT [FK_UserGroup_GroupId]
        ALTER TABLE [dbo].[UserGroup]
            DROP CONSTRAINT [FK_UserGroup_UserId]

        TRUNCATE TABLE [dbo].[GroupRole]
        TRUNCATE TABLE [dbo].[UserGroup]
        TRUNCATE TABLE [dbo].[Role]
        TRUNCATE TABLE [dbo].[Group]
        TRUNCATE TABLE [dbo].[User]

        INSERT INTO [dbo].[User] ([Account], [Password], [Name], [Created])
        VALUES ('admin', 'Ihs3/NtS0PfDm70L4hHbDhwAyl++zVeIeARjAmxrlks=', 'admin', '2018/06/07 21:30')

        INSERT INTO [dbo].[Group] ([Name], [Created])
        VALUES ('Group1', '2018-06-07 21:30:00.0000000')

        SET IDENTITY_INSERT [dbo].[Role] ON
        INSERT INTO [dbo].[Role] ([Id], [Name], [Remark], [Created])
        VALUES (1, 'role1', NULL, '2018-06-07 21:30:00.0000000'),
               (2, 'role2', NULL, '2018-06-07 21:30:00.0000000')
        SET IDENTITY_INSERT [dbo].[Role] OFF

        INSERT INTO [dbo].[UserGroup] ([UserId], [GroupId], [Created])
        VALUES (1, 1, '2018-06-07 21:30:00.0000000')

        INSERT INTO [dbo].[GroupRole] ([GroupId], [RoleId], [Created])
        VALUES (1, 1, '2018-06-07 21:30:00.0000000')

        ALTER TABLE [dbo].[GroupRole]
            WITH CHECK
                ADD CONSTRAINT [FK_GroupRole_RoleId] FOREIGN KEY ([RoleId])
                REFERENCES [dbo].[Role] ([Id])
        ALTER TABLE [dbo].[GroupRole]
            CHECK CONSTRAINT [FK_GroupRole_RoleId]

        ALTER TABLE [dbo].[GroupRole]
            WITH CHECK
                ADD CONSTRAINT [FK_GroupRole_GroupId] FOREIGN KEY ([GroupId])
                REFERENCES [dbo].[Group] ([Id])
        ALTER TABLE [dbo].[GroupRole]
            CHECK CONSTRAINT [FK_GroupRole_GroupId]

        ALTER TABLE [dbo].[UserGroup]
            WITH CHECK
                ADD CONSTRAINT [FK_UserGroup_UserId] FOREIGN KEY ([UserId])
                REFERENCES [dbo].[User] ([Id])
        ALTER TABLE [dbo].[UserGroup]
            CHECK CONSTRAINT [FK_UserGroup_UserId]

        ALTER TABLE [dbo].[UserGroup]
            WITH CHECK
                ADD CONSTRAINT [FK_UserGroup_GroupId] FOREIGN KEY ([GroupId])
                REFERENCES [dbo].[Group] ([Id])
        ALTER TABLE [dbo].[UserGroup]
            CHECK CONSTRAINT [FK_UserGroup_GroupId]
   
    END
