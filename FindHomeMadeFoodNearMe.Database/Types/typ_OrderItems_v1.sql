CREATE TYPE [dbo].[typ_OrderItems_v1] AS TABLE
(
    [DishId] BIGINT NOT NULL,
    [Quantity] INT NOT NULL, 
    [ItemStatus] INT NOT NULL
)
