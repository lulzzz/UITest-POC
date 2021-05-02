CREATE TABLE [Tender].[VacationsDate] (
    [VacationId]   INT             IDENTITY (1, 1) NOT NULL,
    [VacationName] NVARCHAR (1024) NULL,
    [VacationDate] DATETIME2 (7)   NULL,
    CONSTRAINT [PK_VacationsDate] PRIMARY KEY CLUSTERED ([VacationId] ASC)
);

