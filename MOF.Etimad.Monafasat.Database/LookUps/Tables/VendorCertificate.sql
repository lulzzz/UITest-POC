CREATE TABLE [LookUps].[VendorCertificate] (
    [VendorCertificateId] INT             NOT NULL,
    [NameEn]              NVARCHAR (1024) NULL,
    [NameAr]              NVARCHAR (1024) NULL,
    CONSTRAINT [PK_VendorCertificate] PRIMARY KEY CLUSTERED ([VendorCertificateId] ASC)
);

