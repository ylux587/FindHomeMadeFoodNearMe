CREATE PROCEDURE [dbo].[UpdateOrderItemStatus]
    @orderId BIGINT,
    @dishId INT,
    @targetStatus INT
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRAN

    UPDATE [dbo].[OrderItems]
    SET [ItemStatus] = @targetStatus
    WHERE [DishId] = @dishId AND [OrderId] = @orderId

    IF NOT EXISTS(SELECT 1 FROM [OrderItems] WHERE [OrderId] = @orderId AND [ItemStatus] <> 5) -- 5 means item delivered
    BEGIN
        UPDATE [dbo].[Orders]
        SET [Status] = 2
        WHERE [OrderId] = @orderId
    END

    COMMIT TRAN

END
