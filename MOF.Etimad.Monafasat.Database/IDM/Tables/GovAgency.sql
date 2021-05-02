CREATE TABLE [IDM].[GovAgency] (
    [CreatedAt]             DATETIME2 (7)   NOT NULL,
    [CreatedBy]             NVARCHAR (256)  NULL,
    [UpdatedAt]             DATETIME2 (7)   NULL,
    [UpdatedBy]             NVARCHAR (256)  NULL,
    [IsActive]              BIT             NULL,
    [AgencyCode]            NVARCHAR (20)   NOT NULL,
    [NameArabic]            NVARCHAR (256)  NULL,
    [NameEnglish]           NVARCHAR (256)  NULL,
    [CategoryId]            INT             NULL,
    [IsDeleted]             BIT             NOT NULL,
    [IsPrimary]             BIT             NOT NULL,
    [IsUGP]                 BIT             NOT NULL,
    [IsVRO]                 BIT             NOT NULL,
    [PurchaseMethods]       NVARCHAR (MAX)  NULL,
    [RowVersion]            VARBINARY (MAX) NULL,
    [AgencyLogoReferenceId] INT             NULL,
    [IsOldSystem]           BIT             NOT NULL,
    [IsExcepted]            BIT             NOT NULL,
    [MobileNumber]          NVARCHAR (256)  NULL,
    [VROOfficeCode]         NVARCHAR (20)   NULL,
    CONSTRAINT [PK_GovAgency] PRIMARY KEY CLUSTERED ([AgencyCode] ASC),
    CONSTRAINT [FK_GovAgency_GovAgency_VROOfficeCode] FOREIGN KEY ([VROOfficeCode]) REFERENCES [IDM].[GovAgency] ([AgencyCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_GovAgency_VROOfficeCode]
    ON [IDM].[GovAgency]([VROOfficeCode] ASC);

