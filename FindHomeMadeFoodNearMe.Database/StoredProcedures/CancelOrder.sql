CREATE PROCEDURE [dbo].[CancelOrder]
    @orderId BIGINT
AS
BEGIN
    SET NOCOUNT ON

    IF NOT EXISTS(SELECT 1 FROM [dbo].[OrderItems] WHERE [OrderId] = @orderId AND [ItemStatus] >= 2) --2 represents item start cooking
    BEGIN
        UPDATE [dbo].[Orders]
        SET [Status] = 3 -- 3 represents cancelled status of order status
        WHERE [OrderId] = @orderId
    END
END
