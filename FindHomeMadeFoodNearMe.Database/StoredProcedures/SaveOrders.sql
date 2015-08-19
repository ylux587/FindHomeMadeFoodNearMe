CREATE PROCEDURE [dbo].[SaveOrders]
    @orders typ_Orders_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[Orders] AS TARGET
    USING
    (
        SELECT
            [OrderId], 
            [UserId], 
            [OrderDate], 
            [SubTotal], 
            [Tax], 
            [OtherCharges], 
            [Notes],
            [Status]
        FROM @orders
    ) AS SOURCE
    ON (TARGET.[OrderId] = SOURCE.[OrderId])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[UserId] = SOURCE.[UserId], 
            TARGET.[OrderDate] = SOURCE.[OrderDate], 
            TARGET.[SubTotal] = SOURCE.[SubTotal], 
            TARGET.[Tax] = SOURCE.[Tax], 
            TARGET.[OtherCharges] = SOURCE.[OtherCharges], 
            TARGET.[Notes] = SOURCE.[Notes],
            TARGET.[Status] = SOURCE.[Status]
    WHEN NOT MATCHED BY TARGET THEN
        INSERT
        (
            [UserId], 
            [OrderDate], 
            [SubTotal], 
            [Tax], 
            [OtherCharges], 
            [Notes],
            [Status]
        )
        VALUES
        (
            SOURCE.[UserId], 
            SOURCE.[OrderDate], 
            SOURCE.[SubTotal], 
            SOURCE.[Tax], 
            SOURCE.[OtherCharges], 
            SOURCE.[Notes],
            SOURCE.[Status]
        );

END
