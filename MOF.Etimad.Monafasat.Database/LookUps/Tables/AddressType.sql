CREATE TABLE [LookUps].[AddressType] (
    [AddressTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [AddressTypeName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([AddressTypeId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of Address', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AddressType', @level2type = N'COLUMN', @level2name = N'AddressTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of Address type', @level0type = N'SCHEMA', @level0name = N'LookUps', @level1type = N'TABLE', @level1name = N'AddressType', @level2type = N'COLUMN', @level2name = N'AddressTypeName';

