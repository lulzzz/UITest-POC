CREATE TABLE [Enquiry].[EnquiryReply] (
    [CreatedAt]            DATETIME2 (7)  NOT NULL,
    [CreatedBy]            NVARCHAR (256) NULL,
    [UpdatedAt]            DATETIME2 (7)  NULL,
    [UpdatedBy]            NVARCHAR (256) NULL,
    [IsActive]             BIT            NULL,
    [EnquiryReplyId]       INT            IDENTITY (1, 1) NOT NULL,
    [EnquiryReplyMessage]  NVARCHAR (MAX) NULL,
    [EnquiryId]            INT            NOT NULL,
    [EnquiryReplyStatusId] INT            NOT NULL,
    [CommitteeId]          INT            NULL,
    [IsComment]            BIT            NULL,
    CONSTRAINT [PK_EnquiryReply] PRIMARY KEY CLUSTERED ([EnquiryReplyId] ASC),
    CONSTRAINT [FK_EnquiryReply_Committee_CommitteeId] FOREIGN KEY ([CommitteeId]) REFERENCES [Committee].[Committee] ([CommitteeId]),
    CONSTRAINT [FK_EnquiryReply_Enquiry_EnquiryId] FOREIGN KEY ([EnquiryId]) REFERENCES [Enquiry].[Enquiry] ([EnquiryId]),
    CONSTRAINT [FK_EnquiryReply_EnquiryReplyStatus_EnquiryReplyStatusId] FOREIGN KEY ([EnquiryReplyStatusId]) REFERENCES [LookUps].[EnquiryReplyStatus] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_EnquiryReply_CommitteeId]
    ON [Enquiry].[EnquiryReply]([CommitteeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnquiryReply_EnquiryId]
    ON [Enquiry].[EnquiryReply]([EnquiryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_EnquiryReply_EnquiryReplyStatusId]
    ON [Enquiry].[EnquiryReply]([EnquiryReplyStatusId] ASC);

