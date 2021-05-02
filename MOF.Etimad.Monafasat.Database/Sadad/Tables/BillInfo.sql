CREATE TABLE [Sadad].[BillInfo] (
    [CreatedAt]                 DATETIME2 (7)   NOT NULL,
    [CreatedBy]                 NVARCHAR (256)  NULL,
    [UpdatedAt]                 DATETIME2 (7)   NULL,
    [UpdatedBy]                 NVARCHAR (256)  NULL,
    [IsActive]                  BIT             NULL,
    [Id]                        INT             IDENTITY (1, 1) NOT NULL,
    [AgencyCode]                NVARCHAR (50)   NULL,
    [BillInvoiceNumber]         NVARCHAR (50)   NULL,
    [AmountDue]                 NUMERIC (15, 2) NOT NULL,
    [DueDate]                   DATETIME2 (7)   NOT NULL,
    [ExpiryDate]                DATETIME2 (7)   NOT NULL,
    [BillReferenceInfo]         NVARCHAR (100)  NULL,
    [BenChapterNumber]          NVARCHAR (50)   NULL,
    [BenSectionNumber]          NVARCHAR (50)   NULL,
    [BenSequenceNumber]         NVARCHAR (50)   NULL,
    [BenSubDepartmentsCount]    NVARCHAR (50)   NULL,
    [BenSubSectionsCount]       NVARCHAR (50)   NULL,
    [BillStatusId]              INT             NOT NULL,
    [ConditionsBookletID]       INT             NULL,
    [InvitationId]              INT             NULL,
    [SadadBatchId]              NVARCHAR (MAX)  NULL,
    [DisplayLabelAr]            NVARCHAR (100)  NULL,
    [DisplayLabelEn]            NVARCHAR (100)  NULL,
    [ActionStatus]              INT             NULL,
    [ActionReason]              NVARCHAR (MAX)  NULL,
    [POINumber]                 NVARCHAR (50)   NULL,
    [POIType]                   NVARCHAR (50)   NULL,
    [BankPaymentId]             NVARCHAR (50)   NULL,
    [BankId]                    NVARCHAR (50)   NULL,
    [BankBranchId]              NVARCHAR (50)   NULL,
    [PaymentChannel]            NVARCHAR (50)   NULL,
    [BankReversalTransactionID] NVARCHAR (50)   NULL,
    [IntermediatePaymentId]     NVARCHAR (MAX)  NULL,
    [SadadPaymentTranactionId]  NVARCHAR (50)   NULL,
    [PaymentStatusCode]         NVARCHAR (50)   NULL,
    [CurrnetAmount]             MONEY           NULL,
    [PurchaseDate]              DATETIME2 (7)   NULL,
    [BillingAccount]            NVARCHAR (50)   NULL,
    [PaymentMethod]             NVARCHAR (50)   NULL,
    [PaymentReferencefInfo]     NVARCHAR (100)  NULL,
    CONSTRAINT [PK_BillInfo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillInfo_BillStatus_BillStatusId] FOREIGN KEY ([BillStatusId]) REFERENCES [LookUps].[BillStatus] ([BillStatusId]),
    CONSTRAINT [FK_BillInfo_ConditionsBooklet_ConditionsBookletID] FOREIGN KEY ([ConditionsBookletID]) REFERENCES [Tender].[ConditionsBooklet] ([ConditionsBookletId]),
    CONSTRAINT [FK_BillInfo_Invitation_InvitationId] FOREIGN KEY ([InvitationId]) REFERENCES [Invitation].[Invitation] ([InvitationId])
);


GO
CREATE NONCLUSTERED INDEX [IX_BillInfo_BillStatusId]
    ON [Sadad].[BillInfo]([BillStatusId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BillInfo_ConditionsBookletID]
    ON [Sadad].[BillInfo]([ConditionsBookletID] ASC) WHERE ([ConditionsBookletID] IS NOT NULL);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_BillInfo_InvitationId]
    ON [Sadad].[BillInfo]([InvitationId] ASC) WHERE ([InvitationId] IS NOT NULL);

