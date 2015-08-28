CREATE PROCEDURE [dbo].[GetOrderItems]
AS
BEGIN
    SET NOCOUNT ON

    SELECT 
        [OrderId],
        [DishId],
        [Quantity],
        [ItemStatus]
    FROM [dbo].[OrderItems]

END