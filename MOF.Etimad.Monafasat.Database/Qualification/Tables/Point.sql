CREATE TABLE [Qualification].[Point] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [PointValue] INT           NOT NULL,
    CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED ([ID] ASC)
);

