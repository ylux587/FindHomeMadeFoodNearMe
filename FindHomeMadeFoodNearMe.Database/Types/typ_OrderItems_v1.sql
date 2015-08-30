CREATE TYPE [dbo].[typ_OrderItems_v1] AS TABLE
(
    [OrderId] BIGINT NOT NULL,
    [DishId] BIGINT NOT NULL,
    [Quantity] INT NOT NULL, 
    [ItemStatus] INT NOT NULL
)
