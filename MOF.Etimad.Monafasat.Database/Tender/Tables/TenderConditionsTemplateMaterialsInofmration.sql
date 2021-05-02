CREATE TABLE [Tender].[TenderConditionsTemplateMaterialsInofmration] (
    [CreatedAt]                                     DATETIME2 (7)  NOT NULL,
    [CreatedBy]                                     NVARCHAR (256) NULL,
    [UpdatedAt]                                     DATETIME2 (7)  NULL,
    [UpdatedBy]                                     NVARCHAR (256) NULL,
    [IsActive]                                      BIT            NULL,
    [TenderConditionsTemplateMaterialInofmrationId] INT            IDENTITY (1, 1) NOT NULL,
    [BasicInformation]                              NVARCHAR (MAX) NULL,
    [RequiredDcoumentationBefore]                   NVARCHAR (MAX) NULL,
    [Tests]                                         NVARCHAR (MAX) NULL,
    [IntilizationAndStartWork]                      NVARCHAR (MAX) NULL,
    [RequiredDcoumentationAfter]                    NVARCHAR (MAX) NULL,
    [Trainging]                                     NVARCHAR (MAX) NULL,
    [Guarantee]                                     NVARCHAR (MAX) NULL,
    [Maintanance]                                   NVARCHAR (MAX) NULL,
    [MachineGuarantee]                              NVARCHAR (MAX) NULL,
    [MachineMaintanance]                            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TenderConditionsTemplateMaterialsInofmration] PRIMARY KEY CLUSTERED ([TenderConditionsTemplateMaterialInofmrationId] ASC)
);

