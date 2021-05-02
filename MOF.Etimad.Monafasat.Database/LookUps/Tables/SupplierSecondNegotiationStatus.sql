CREATE TABLE [LookUps].[SupplierSecondNegotiationStatus] (
    [SupplierNegotiaitionStatusId] INT            NOT NULL,
    [Name]                         NVARCHAR (100) NULL,
    CONSTRAINT [PK_SupplierSecondNegotiationStatus] PRIMARY KEY CLUSTERED ([SupplierNegotiaitionStatusId] ASC)
);

