CREATE PROCEDURE [dbo].[UpdateOrderItemStatus]
    @orderId BIGINT,
    @dishId BIGINT,
    @providerId BIGINT,
    @targetStatus INT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRAN

    UPDATE oi
    SET oi.[ItemStatus] = @targetStatus
    FROM [dbo].[OrderItems] oi
        INNER JOIN [dbo].[Dishes] d ON oi.[DishId] = d.[DishId]
    WHERE oi.[DishId] = @dishId AND oi.[OrderId] = @orderId AND d.[ProviderId] = @providerId

    IF NOT EXISTS(SELECT 1 FROM [OrderItems] WHERE [OrderId] = @orderId AND [ItemStatus] <> 5) -- 5 means item delivered
    BEGIN
        UPDATE [dbo].[Orders]
        SET [Status] = 2
        WHERE [OrderId] = @orderId
    END

    COMMIT TRAN

END
