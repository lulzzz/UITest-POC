CREATE TABLE [Tender].[Address] (
    [AddressId]     INT             IDENTITY (1, 1) NOT NULL,
    [AddressName]   NVARCHAR (1024) NULL,
    [AddressTypeId] INT             NOT NULL,
    [BranchId]      INT             NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC),
    CONSTRAINT [FK_Address_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Address_BranchId]
    ON [Tender].[Address]([BranchId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define identity of Address', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'AddressId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define name of Address', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'AddressName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define type of Address', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'AddressTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define forigne key of branch', @level0type = N'SCHEMA', @level0name = N'Tender', @level1type = N'TABLE', @level1name = N'Address', @level2type = N'COLUMN', @level2name = N'BranchId';

