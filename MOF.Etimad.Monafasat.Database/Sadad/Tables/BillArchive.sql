CREATE TABLE [Sadad].[BillArchive] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [CreatedAt]             DATETIME2 (7)  NOT NULL,
    [CreatedBy]             NVARCHAR (256) NULL,
    [UpdatedAt]             DATETIME2 (7)  NULL,
    [UpdatedBy]             NVARCHAR (256) NULL,
    [IsActive]              BIT            NULL,
    [ConditionsBookletID]   INT            NULL,
    [InvitationId]          INT            NULL,
    [TenderId]              INT            NOT NULL,
    [TenderReferenceNumber] NVARCHAR (100) NULL,
    [BillInvoiceNumber]     NVARCHAR (50)  NULL,
    [AgencyCode]            NVARCHAR (50)  NULL,
    [SupplierNumber]        NVARCHAR (20)  NULL,
    CONSTRAINT [PK_BillArchive] PRIMARY KEY CLUSTERED ([Id] ASC)
);

