CREATE TABLE [Branch].[BranchAddresse] (
    [CreatedAt]       DATETIME2 (7)   NOT NULL,
    [CreatedBy]       NVARCHAR (256)  NULL,
    [UpdatedAt]       DATETIME2 (7)   NULL,
    [UpdatedBy]       NVARCHAR (256)  NULL,
    [IsActive]        BIT             NULL,
    [BranchAddressId] INT             IDENTITY (1, 1) NOT NULL,
    [Position]        NVARCHAR (1024) NULL,
    [Address]         NVARCHAR (1024) NULL,
    [Phone]           NVARCHAR (100)  NULL,
    [Fax]             NVARCHAR (100)  NULL,
    [Phone2]          NVARCHAR (100)  NULL,
    [Fax2]            NVARCHAR (100)  NULL,
    [Email]           NVARCHAR (200)  NULL,
    [Description]     NVARCHAR (1024) NULL,
    [AddressName]     NVARCHAR (1024) NULL,
    [CityCode]        NVARCHAR (1024) NULL,
    [PostalCode]      NVARCHAR (1024) NULL,
    [ZipCode]         NVARCHAR (1024) NULL,
    [AddressTypeId]   INT             NOT NULL,
    [BranchId]        INT             NULL,
    CONSTRAINT [PK_BranchAddresse] PRIMARY KEY CLUSTERED ([BranchAddressId] ASC),
    CONSTRAINT [FK_BranchAddresse_AddressType_AddressTypeId] FOREIGN KEY ([AddressTypeId]) REFERENCES [LookUps].[AddressType] ([AddressTypeId]),
    CONSTRAINT [FK_BranchAddresse_Branch_BranchId] FOREIGN KEY ([BranchId]) REFERENCES [Branch].[Branch] ([BranchId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BranchAddresse_AddressTypeId]
    ON [Branch].[BranchAddresse]([AddressTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BranchAddresse_BranchId]
    ON [Branch].[BranchAddresse]([BranchId] ASC);

