CREATE TABLE [Tender].[TenderAgreementAgency] (
    [CreatedAt]               DATETIME2 (7)  NOT NULL,
    [CreatedBy]               NVARCHAR (256) NULL,
    [UpdatedAt]               DATETIME2 (7)  NULL,
    [UpdatedBy]               NVARCHAR (256) NULL,
    [IsActive]                BIT            NULL,
    [TenderAgreementAgencyId] INT            IDENTITY (1, 1) NOT NULL,
    [AgencyCode]              NVARCHAR (20)  NULL,
    [TenderId]                INT            NOT NULL,
    CONSTRAINT [PK_TenderAgreementAgency] PRIMARY KEY CLUSTERED ([TenderAgreementAgencyId] ASC),
    CONSTRAINT [FK_TenderAgreementAgency_GovAgency_AgencyCode] FOREIGN KEY ([AgencyCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode]),
    CONSTRAINT [FK_TenderAgreementAgency_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_TenderAgreementAgency_AgencyCode]
    ON [Tender].[TenderAgreementAgency]([AgencyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TenderAgreementAgency_TenderId]
    ON [Tender].[TenderAgreementAgency]([TenderId] ASC);

