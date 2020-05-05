DROP PROCEDURE IF EXISTS [dbo].[usp_AuthorizeUserIdRoles];
GO

CREATE PROCEDURE [dbo].[usp_AuthorizeUserIdRoles] @UserId INT,
                                                  @Roles  [dbo].[Nvarchar1000] READONLY
AS

DECLARE @result TABLE
                (
                    [UserId]     INT,
                    [AuthResult] BIT
                )

INSERT INTO @result
SELECT @UserId                            AS [UserId],
       IIF([roleCount].[Count] > 0, 1, 0) AS [AuthResult]
FROM (
         SELECT COUNT(0) AS [Count]
         FROM [dbo].[User] [u]
             JOIN [UserGroup] [ug]
                  ON [u].[Id] = [ug].[UserId]
             JOIN [GroupRole] [gr]
                  ON [ug].[Created] = [gr].[Created]
             JOIN [Role] [r]
                  ON [gr].[RoleId] = [r].[Id]
             JOIN @Roles [r2]
                  ON [r].[Name] = [r2].[Value]
         WHERE [u].[Id] = @UserId
     ) [roleCount]

SELECT *
FROM @result

GO