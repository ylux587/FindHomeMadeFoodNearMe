CREATE PROCEDURE [dbo].[SaveOrder]
    @userId BIGINT,
    @orderDate DATETIME2(7),
    @subTotal DECIMAL(7,2),
    @tax DECIMAL(7,2) = NULL,
    @otherCharges DECIMAL(7,2) = NULL,
    @notes NVARCHAR(1000) = NULL,
    @status INT,
    @orderItems typ_OrderItems_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    BEGIN TRAN

    INSERT [dbo].[Orders]
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
            @userId, 
            @orderDate, 
            @subTotal, 
            @tax, 
            @otherCharges, 
            @notes,
            @status
        )
     DECLARE @orderId BIGINT = SCOPE_IDENTITY();

     EXEC SaveOrderItems @orderId, @orderItems

     SELECT @orderId

     COMMIT TRAN

END
