CREATE TABLE [Enquiry].[JoinTechnicalCommittee] (
    [CreatedAt]                DATETIME2 (7)  NOT NULL,
    [CreatedBy]                NVARCHAR (256) NULL,
    [UpdatedAt]                DATETIME2 (7)  NULL,
    [UpdatedBy]                NVARCHAR (256) NULL,
    [IsActive]                 BIT            NULL,
    [JoinTechnicalCommitteeId] INT            IDENTITY (1, 1) NOT NULL,
    [EnquiryId]                INT            NOT NULL,
    [CommitteeId]              INT            NOT NULL,
    CONSTRAINT [PK_JoinTechnicalCommittee] PRIMARY KEY CLUSTERED ([JoinTechnicalCommitteeId] ASC),
    CONSTRAINT [FK_JoinTechnicalCommittee_Committee_CommitteeId] FOREIGN KEY ([CommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_JoinTechnicalCommittee_Enquiry_EnquiryId] FOREIGN KEY ([EnquiryId]) REFERENCES [Enquiry].[Enquiry] ([EnquiryId])
);


GO
CREATE NONCLUSTERED INDEX [IX_JoinTechnicalCommittee_CommitteeId]
    ON [Enquiry].[JoinTechnicalCommittee]([CommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_JoinTechnicalCommittee_EnquiryId]
    ON [Enquiry].[JoinTechnicalCommittee]([EnquiryId] ASC);

