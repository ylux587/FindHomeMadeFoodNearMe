CREATE PROCEDURE [dbo].[GetDishes]
AS
BEGIN
    SET NOCOUNT ON

    SELECT
        [DishId], 
        [ProviderId], 
        [DishType], 
        [DishName], 
        [Description], 
        [Ingredients], 
        [Price], 
        [WaitingTimeInMins], 
        [ThumbNailPictureKey], 
        [Available]
    FROM [dbo].[Dishes]

END
