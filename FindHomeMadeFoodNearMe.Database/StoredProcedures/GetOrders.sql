CREATE PROCEDURE [dbo].[GetOrders]
AS
BEGIN
    SET NOCOUNT ON

    SELECT
        [OrderId], 
        [UserId], 
        [OrderDate], 
        [SubTotal], 
        [Tax], 
        [OtherCharges], 
        [Notes],
        [Status]
    FROM [dbo].[Orders]

END