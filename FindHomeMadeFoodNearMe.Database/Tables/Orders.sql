CREATE TABLE [dbo].[Orders]
(
    [OrderId] BIGINT NOT NULL IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [OrderDate] DATETIME2 NOT NULL, 
    [SubTotal] DECIMAL(7, 2) NOT NULL, 
    [Tax] DECIMAL(7, 2) NULL, 
    [OtherCharges] DECIMAL(7, 2) NULL, 
    [Notes] NVARCHAR(1000) NULL, 
    [Status] INT NOT NULL, 
    CONSTRAINT [PK_Orders] PRIMARY KEY ([OrderId]) 
)
