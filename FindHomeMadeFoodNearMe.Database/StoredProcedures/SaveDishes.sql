CREATE PROCEDURE [dbo].[SaveDishes]
    @dishes typ_Dishes_v1 READONLY
AS
BEGIN
    SET NOCOUNT ON

    MERGE INTO [dbo].[Dishes] AS TARGET
    USING 
    (
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
        FROM @dishes
    ) AS SOURCE
    ON (SOURCE.[DishId] = TARGET.[DishId])
    WHEN MATCHED THEN
        UPDATE SET
            TARGET.[ProviderId] = SOURCE.[ProviderId],
            TARGET.[DishType] = SOURCE.[DishType],
            TARGET.[DishName] = SOURCE.[DishName],
            TARGET.[Description] = SOURCE.[Description],
            TARGET.[Ingredients] = SOURCE.[Ingredients],
            TARGET.[Price] = SOURCE.[Price],
            TARGET.[WaitingTimeInMins] = SOURCE.[WaitingTimeInMins],
            TARGET.[ThumbNailPictureKey] = SOURCE.[ThumbNailPictureKey],
            TARGET.[Available] = SOURCE.[Available]
    WHEN NOT MATCHED THEN
        INSERT
        (
            [ProviderId], 
            [DishType], 
            [DishName], 
            [Description], 
            [Ingredients], 
            [Price], 
            [WaitingTimeInMins], 
            [ThumbNailPictureKey], 
            [Available]
        )
        VALUES
        (
            SOURCE.[ProviderId], 
            SOURCE.[DishType], 
            SOURCE.[DishName], 
            SOURCE.[Description], 
            SOURCE.[Ingredients], 
            SOURCE.[Price], 
            SOURCE.[WaitingTimeInMins], 
            SOURCE.[ThumbNailPictureKey], 
            SOURCE.[Available]
        );

END
