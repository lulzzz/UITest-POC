CREATE TABLE [Verification].[VerificationType] (
    [VerificationTypeId]   INT             IDENTITY (1, 1) NOT NULL,
    [VerificationTypeName] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_VerificationType] PRIMARY KEY CLUSTERED ([VerificationTypeId] ASC)
);

