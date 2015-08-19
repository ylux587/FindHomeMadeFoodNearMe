CREATE PROCEDURE [dbo].[GetOrderItems]
AS
BEGIN
    SET NOCOUNT ON

    SELECT 
        [OrderId],
        [DishId],
        [ItemStatus]
    FROM [dbo].[OrderItems]

END