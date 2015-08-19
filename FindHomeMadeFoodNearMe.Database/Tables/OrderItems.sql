CREATE TABLE [dbo].[OrderItems]
(
    [OrderId] BIGINT NOT NULL, 
    [DishId] BIGINT NOT NULL, 
    [ItemStatus] INT NOT NULL, 
    CONSTRAINT [PK_OrderItems] PRIMARY KEY ([OrderId], [DishId]) 
)
