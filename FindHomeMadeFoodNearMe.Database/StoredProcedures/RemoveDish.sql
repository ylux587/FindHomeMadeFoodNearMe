CREATE PROCEDURE [dbo].[RemoveDish]
    @dishId INT,
    @providerId BIGINT
AS
BEGIN
    SET NOCOUNT ON

    UPDATE [dbo].[Dishes]
    SET [Available] = 0
    WHERE [DishId] = @dishId AND [ProviderId] = @providerId

END
