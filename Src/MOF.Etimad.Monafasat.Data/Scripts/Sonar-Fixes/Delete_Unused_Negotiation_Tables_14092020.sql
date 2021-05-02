IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

DROP TABLE [CommunicationRequest].[NegotiationQuantityTableItem];

GO

DROP TABLE [CommunicationRequest].[NegotiationQuantityTable];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200914083736_Delete_Unused_Negotiation_Tables', N'3.1.0');

GO

