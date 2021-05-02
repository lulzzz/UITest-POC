CREATE TABLE [Enquiry].[Enquiry] (
    [CreatedAt]                    DATETIME2 (7)  NOT NULL,
    [CreatedBy]                    NVARCHAR (256) NULL,
    [UpdatedAt]                    DATETIME2 (7)  NULL,
    [UpdatedBy]                    NVARCHAR (256) NULL,
    [IsActive]                     BIT            NULL,
    [EnquiryId]                    INT            IDENTITY (1, 1) NOT NULL,
    [EnquiryName]                  NVARCHAR (MAX) NULL,
    [CommericalRegisterNo]         NVARCHAR (20)  NULL,
    [TenderId]                     INT            NOT NULL,
    [AgencyCommunicationRequestId] INT            NOT NULL,
    CONSTRAINT [PK_Enquiry] PRIMARY KEY CLUSTERED ([EnquiryId] ASC),
    CONSTRAINT [FK_Enquiry_AgencyCommunicationRequest_AgencyCommunicationRequestId] FOREIGN KEY ([AgencyCommunicationRequestId]) REFERENCES [CommunicationRequest].[AgencyCommunicationRequest] ([AgencyRequestId]),
    CONSTRAINT [FK_Enquiry_Supplier_CommericalRegisterNo] FOREIGN KEY ([CommericalRegisterNo]) REFERENCES [IDM].[Supplier] ([SelectedCr]),
    CONSTRAINT [FK_Enquiry_Tender_TenderId] FOREIGN KEY ([TenderId]) REFERENCES [Tender].[Tender] ([TenderId])
);


GO
CREATE NONCLUSTERED INDEX [IX_Enquiry_AgencyCommunicationRequestId]
    ON [Enquiry].[Enquiry]([AgencyCommunicationRequestId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Enquiry_CommericalRegisterNo]
    ON [Enquiry].[Enquiry]([CommericalRegisterNo] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Enquiry_TenderId]
    ON [Enquiry].[Enquiry]([TenderId] ASC);

