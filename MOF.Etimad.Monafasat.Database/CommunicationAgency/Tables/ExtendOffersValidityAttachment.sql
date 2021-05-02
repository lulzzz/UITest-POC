CREATE TABLE [CommunicationAgency].[ExtendOffersValidityAttachment] (
    [CreatedAt]                      DATETIME2 (7)   NOT NULL,
    [CreatedBy]                      NVARCHAR (256)  NULL,
    [UpdatedAt]                      DATETIME2 (7)   NULL,
    [UpdatedBy]                      NVARCHAR (256)  NULL,
    [IsActive]                       BIT             NULL,
    [AttachmentId]                   INT             IDENTITY (1, 1) NOT NULL,
    [ExtendOffersValiditySupplierId] INT             NOT NULL,
    [Name]                           NVARCHAR (200)  NOT NULL,
    [AttachmentTypeId]               INT             NOT NULL,
    [FileNetReferenceId]             NVARCHAR (1024) NULL,
    CONSTRAINT [PK_ExtendOffersValidityAttachment] PRIMARY KEY CLUSTERED ([AttachmentId] ASC),
    CONSTRAINT [FK_ExtendOffersValidityAttachment_AttachmentType_AttachmentTypeId] FOREIGN KEY ([AttachmentTypeId]) REFERENCES [LookUps].[AttachmentType] ([AttachmentTypeId]),
    CONSTRAINT [FK_ExtendOffersValidityAttachment_ExtendOffersValiditySupplier_ExtendOffersValiditySupplierId] FOREIGN KEY ([ExtendOffersValiditySupplierId]) REFERENCES [CommunicationRequest].[ExtendOffersValiditySupplier] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ExtendOffersValidityAttachment_AttachmentTypeId]
    ON [CommunicationAgency].[ExtendOffersValidityAttachment]([AttachmentTypeId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ExtendOffersValidityAttachment_ExtendOffersValiditySupplierId]
    ON [CommunicationAgency].[ExtendOffersValidityAttachment]([ExtendOffersValiditySupplierId] ASC);

