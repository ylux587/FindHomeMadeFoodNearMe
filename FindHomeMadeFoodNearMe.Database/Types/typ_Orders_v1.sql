CREATE TYPE [dbo].[typ_Orders_v1] AS TABLE
(
    [OrderId] BIGINT NOT NULL, 
    [UserId] BIGINT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [SubTotal] DECIMAL(7, 2) NOT NULL, 
    [Tax] DECIMAL(7, 2) NULL, 
    [OtherCharges] DECIMAL(7, 2) NULL, 
    [Notes] NVARCHAR(1000) NULL,
    [Status] INT NOT NULL
)
