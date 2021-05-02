CREATE TABLE [Invitation].[Invitation] (
    [CreatedAt]              DATETIME2 (7)  NOT NULL,
    [CreatedBy]              NVARCHAR (256) NULL,
    [UpdatedAt]              DATETIME2 (7)  NULL,
    [UpdatedBy]              NVARCHAR (256) NULL,
    [IsActive]               BIT            NULL,
    [InvitationId]           INT            IDENTITY (1, 1) NOT NULL,
    [RejectionReason]        NVARCHAR (MAX) NULL,
    [SubmissionDate]         DATETIME2 (7)  NULL,
    [SendingDate]            DATETIME2 (7)  NULL,
    [WithdrawalDate]         DATETIME2 (7)  NULL,
    [InvitedByQualification] BIT            NULL,
    [SupplierType]           INT            NULL,
    [CommericalRegisterNo]   NVARCHAR (20)  NULL,
    [TenderId]               INT            NOT NULL,
    [StatusId]               INT            NOT NULL,
    [InvitationTypeId]       INT            NOT NULL,
    CONSTRAINT [PK_Invitation] PRIMARY KEY CLUSTERED ([InvitationId] ASC),
    CONSTRAINT [FK_Invitation_InvitationStatus_StatusId] FOREIGN KEY ([StatusId]) REFERENCES [LookUps].[InvitationStatus] ([InvitationStatusId]),
    CONSTRAINT [FK_Invitation_InvitationType_InvitationTypeId] FOREIGN KEY ([InvitationTypeId]) REFERENCES [LookUps].[InvitationType] ([InvitationTypeId]),
    CONSTRAINT [FK_Invitation_Supplier_CommericalRegisterNo] FOREIGN KEY ([CommericalRegisterNo]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_Invitation_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Invitation_CommericalRegisterNo]
    ON [Invitation].[Invitation]([CommericalRegisterNo] ASC)
    INCLUDE([TenderId], [InvitationId]);


GO
CREATE NONCLUSTERED INDEX [IX_Invitation_InvitationTypeId]
    ON [Invitation].[Invitation]([InvitationTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Invitation_StatusId]
    ON [Invitation].[Invitation]([StatusId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Invitation_TenderId]
    ON [Invitation].[Invitation]([TenderId] ASC);

