CREATE TABLE [Verification].[VerificationCode] (
    [CreatedAt]          DATETIME2 (7)   NOT NULL,
    [CreatedBy]          NVARCHAR (256)  NULL,
    [UpdatedAt]          DATETIME2 (7)   NULL,
    [UpdatedBy]          NVARCHAR (256)  NULL,
    [IsActive]           BIT             NULL,
    [VerificationCodeId] INT             IDENTITY (1, 1) NOT NULL,
    [VerificationCodeNo] NVARCHAR (1024) NULL,
    [CodeTypeId]         INT             NOT NULL,
    [VerificationTypeId] INT             NOT NULL,
    [UserId]             INT             NOT NULL,
    [ExpiredDate]        DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_VerificationCode] PRIMARY KEY CLUSTERED ([VerificationCodeId] ASC),
    CONSTRAINT [FK_VerificationCode_VerificationType_VerificationTypeId] FOREIGN KEY ([VerificationTypeId]) REFERENCES [Verification].[VerificationType] ([VerificationTypeId])
);


GO
CREATE NONCLUSTERED INDEX [IX_VerificationCode_VerificationTypeId]
    ON [Verification].[VerificationCode]([VerificationTypeId] ASC);

