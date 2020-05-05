IF type_id('[dbo].[Nvarchar1000]') IS NOT NULL
    DROP TYPE [dbo].[Nvarchar1000];
GO

CREATE TYPE [dbo].[Nvarchar1000] AS TABLE
(
    [Value]  NVARCHAR(1000) NULL,
    [Serial] BIGINT         NOT NULL
);
GO