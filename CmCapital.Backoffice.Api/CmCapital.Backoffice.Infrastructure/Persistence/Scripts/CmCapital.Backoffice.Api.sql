IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [RegistrationDate] datetime2 NOT NULL,
    [UnitValue] real NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230525232907_InitialCreate', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Products] ADD [Category] nvarchar(100) NOT NULL DEFAULT N'';
GO

CREATE TABLE [Clients] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(1000) NOT NULL,
    [LastPurchase] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000',
    [Balance] real NOT NULL,
    [InitialValue] real NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230526064850_AddClient', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchases] (
    [Id] uniqueidentifier NOT NULL,
    [ClientId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [UnitValue] real NOT NULL,
    [Amount] real NOT NULL,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230526081743_AddPurchase', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Purchases] ADD [CreateAt] datetime2 NOT NULL DEFAULT '2023-05-26T14:03:48.8308899-03:00';
GO

CREATE TABLE [Taxs] (
    [Id] uniqueidentifier NOT NULL,
    [InitialValue] decimal(18,2) NOT NULL,
    [FinalValue] decimal(18,2) NOT NULL,
    [Percentage] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Taxs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230526170348_AddTax', N'7.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Taxs] ADD [CreateAt] datetime2 NOT NULL DEFAULT '2023-05-26T15:13:11.8527738-03:00';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchases]') AND [c].[name] = N'CreateAt');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Purchases] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Purchases] ADD DEFAULT '2023-05-26T15:13:11.8527007-03:00' FOR [CreateAt];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230526181312_AddTax1', N'7.0.5');
GO

COMMIT;
GO

INSERT INTO [dbo].[Taxs]
           ([Id]
           ,[InitialValue]
           ,[FinalValue]
           ,[Percentage]
           ,[CreateAt])
     VALUES
           (NEWID()
           ,500
           ,1000
           ,0.65
           , GETDATE())
GO

INSERT INTO [dbo].[Taxs]
           ([Id]
           ,[InitialValue]
           ,[FinalValue]
           ,[Percentage]
           ,[CreateAt])
     VALUES
           (NEWID()
           ,1001
           ,10000
           ,0.85
           , GETDATE())
GO


INSERT INTO [dbo].[Taxs]
           ([Id]
           ,[InitialValue]
           ,[FinalValue]
           ,[Percentage]
           ,[CreateAt])
     VALUES
           (NEWID()
           ,10001
           ,20000.00
           ,0.98
           , GETDATE())
GO

INSERT INTO [dbo].[Taxs]
           ([Id]
           ,[InitialValue]
           ,[FinalValue]
           ,[Percentage]
           ,[CreateAt])
     VALUES
           (NEWID()
           ,20001
           ,999999999
           ,1.02
           , GETDATE())
GO