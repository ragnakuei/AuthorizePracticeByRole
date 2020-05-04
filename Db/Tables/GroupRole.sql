CREATE TABLE [dbo].[GroupRole](
	[GroupId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Created] [datetime2](7) NOT NULL,
    PRIMARY KEY CLUSTERED 
    (
        [GroupId] ASC,
        [RoleId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([Id])
GO

ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_GroupId]
GO

ALTER TABLE [dbo].[GroupRole]  WITH CHECK ADD  CONSTRAINT [FK_GroupRole_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO

ALTER TABLE [dbo].[GroupRole] CHECK CONSTRAINT [FK_GroupRole_RoleId]
GO