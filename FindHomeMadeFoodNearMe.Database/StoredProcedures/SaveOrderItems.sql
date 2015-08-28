CREATE PROCEDURE [dbo].[SaveOrderItems]
    @orderId    BIGINT,
    @orderItems typ_OrderItems_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[OrderItems] AS TARGET
    USING
    (
        SELECT
            [DishId],
            [Quantity], 
            [ItemStatus]
        FROM @orderItems
    ) AS SOURCE
    ON (TARGET.[OrderId] = @orderId AND TARGET.[DishId] = SOURCE.[DishId])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[OrderId] = @orderId, 
            TARGET.[DishId] = SOURCE.[DishId],
            TARGET.[Quantity] = SOURCE.[Quantity], 
            TARGET.[ItemStatus] = SOURCE.[ItemStatus]
    WHEN NOT MATCHED BY TARGET THEN
        INSERT
        (
            [OrderId],
            [DishId],
            [Quantity],
            [ItemStatus]
        )
        VALUES
        (
            @orderId,
            SOURCE.[DishId],
            SOURCE.[Quantity],
            SOURCE.[ItemStatus]
        )
    WHEN NOT MATCHED BY SOURCE AND TARGET.[OrderId] = @orderId THEN
        DELETE;

END
